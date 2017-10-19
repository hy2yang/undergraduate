library(stringr)
library(ggplot2)
library(plyr)
library(MASS)
setwd('D:/BA/Homework/HW01')
Air <- read.csv('Airfares.csv')

#����ĺ����ֽ�������factor�ͱ���ַ��ͣ�����ȥ���ַ����������е�$��,���ţ���󽫱��������ֵ�͡�
Factor2Num <- function(x)
{ AA <- as.character(x);
  pattern <- '[$,]+';
  AA <- as.numeric((str_replace_all(AA,pattern,'')));
  return (AA)                                             }

Air[,'S_INCOME'] <- Factor2Num (Air[,'S_INCOME']) #��S_INCOME������ԭ����factor��ת������ֵ�ͱ�����
Air[,'E_INCOME'] <- Factor2Num (Air[,'E_INCOME'])
Air[,'FARE'] <- Factor2Num (Air[,'FARE'])

#(i)�����Ʊ�۸����ֵ��Ԥ�����ӵ����ϵ����ָ���ĸ���ֵ��Ԥ�����Ӻͻ�Ʊ�۸���������
#�Ը�Ԥ������Ϊ���ᣬ�������������Ʊ�۸��ɢ��ͼ��

length(sapply(Air,is.numeric))         #air�ܹ��ж�������numeric
numcols<-sapply(Air,is.numeric)[-18]   #��ȥFARE�Լ���ɸѡ����
cor(Air$FARE,Air[,numcols])            #�������ϵ�� 
qplot(DISTANCE,FARE,data=Air)          #������۸�ɢ��ͼ

#(ii)̽����Ʊ�۸��4������ͱ���VACATION��SW��SLOT��GATE�Ĺ�ϵ��
#����ÿ������ͱ����У���Ʊ�����ڸ�����ͱ�����ʾ�ĸ�����и��м�����
#�ڸ�������е�ƽ����Ʊ�۸��Ƕ��١�ָ���ĸ�����ͱ����Ի�Ʊ�۸��Ԥ�������á�

table(Air$VACATION)   #no=468 yes=170
table(Air$SW)         #no=444 yes=194
table(Air$SLOT)       #controlled=182 free=456
table(Air$GATE)       #constrained=124 free=514

aggregate(FARE~VACATION, data=Air, mean) 
aggregate(FARE~SW, data=Air, mean)
aggregate(FARE~SLOT, data=Air, mean)
aggregate(FARE~GATE, data=Air, mean)     #�ɼ�SW��ɵ�ƽ���۸�䶯���Ӧ����Ԥ����������


#(iii)ʹ��Ԥ������COUPON��NEW��HI��S_INCOME��E_INCOME��S_POP��E_POP��DISTANCE��PAX��
#SW��VACATION��������ģ�ͣ����û�Ʊ�۸�FARE��Ϊ�����ͱ��������������ݽ��лع������
#ָ����Щ������1%��������ˮƽ����������������������ͻع����������ñ������Ӱ���Ʊ�۸�

myfit <- lm(FARE~COUPON+NEW+HI+S_INCOME+E_INCOME+S_POP+E_POP+DISTANCE+PAX+SW+VACATION,
            data=Air)
summary(myfit)               
#1%��������ˮƽ�£�HI��S_INCOME��E_INCOME��S_POP��E_POP��DISTANCE��PAX��SW��VACATION����
#HI �г����ж�ÿ����1����Ʊ���۴�Լ����0.087��
#S_INCOME ��ɳ��е��˾�����ÿ����1����Ʊ���۴�Լ����0.016��
#E_INCOME �յ���е��˾�����ÿ����1����Ʊ���۴�Լ����0.015��
#S_POP  ��ɳ��е��˿�ÿ����1����Ʊ���۴�Լ����4.452e-06��
#E_POP  �յ���е��˿�ÿ����1����Ʊ���۴�Լ����5.749e-06��
#DISTANCE ���о���ÿ����1����Ʊ���۴�Լ����0.071��
#PAX  �ú��ߵĳ˿�����ÿ����1����Ʊ���۴�Լ�½�9.028e-04��
#SW   ���Ϻ�����Ӫ�ú��ߣ����Ʊ���۴�Լ�½�46.68��
#VACATION �����κ��ߣ����Ʊ���۴�Լ�½�35.59��


#(iv) ����iii�������õ�ģ����Ϊfull model��ʹ��ǰ��ѡ�񷨣�forward selection��ѡ�������
# д�����յ�ģ�ͣ���ʹ������ģ�ͽ������Իع�����õ�����������ϵ����

min_model = lm (FARE ~ 1, data = Air)
full_model<-formula(myfit)
stepf <- stepAIC(min_model, direction="forward",scope=full_model)
stepf$coefficients
summary(stepf)

#����ģ��FARE ~ DISTANCE + SW + VACATION + HI + E_POP + S_POP + PAX + E_INCOME + S_INCOME
#ϵ��    (Intercept)      DISTANCE         SWYes   VACATIONYes            HI         E_POP         S_POP 
#      -3.419061e+01  7.257997e-02 -4.687025e+01 -3.560884e+01  8.178041e-03  5.792902e-06  4.445012e-06 
#                PAX      E_INCOME      S_INCOME 
#      -9.445595e-04  1.470679e-03  1.595628e-03 

#��v�������ݽ���������飬 70%����ѵ����30%������֤��ʹ�õ�����ģ���ǣ�iv���õ�������ģ�ͣ�
# �������ǰ����set.seed(1000)���ֱ����ѵ��������֤���õ���RMS Error����֤���õ���RMS Error
# �Ƿ����Դ���ѵ������RMS Error����󻭳���֤���õ��Ĳв��Box Plot��

set.seed(1000)
SampleIndex <- sample(1:nrow(Air),round(nrow(Air)*0.7),replace = FALSE)
AirSample<-Air[SampleIndex,]
fit_training <- lm(FARE ~ DISTANCE + SW + VACATION + HI + E_POP + S_POP + PAX + E_INCOME + S_INCOME
                   , data = AirSample)
summary(fit_training)
sum(fit_training$residuals^2)  
sqrt(sum(fit_training$residuals^2)/nrow(AirSample))   #ѵ��RMS Error=36.99157

AirValidation<- Air[-SampleIndex,]
validation_result = predict(fit_training,newdata=AirValidation)
Vresiduals = AirValidation[,"FARE"]-validation_result
sum(Vresiduals^2)
sqrt(sum(Vresiduals^2)/length(Vresiduals))      #��֤RMS Error=34.56452 ������������ѵ��RMS Error
boxplot(Vresiduals)