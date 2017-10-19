library(stringr)
library(ggplot2)
library(plyr)
library(MASS)
setwd('D:/BA/Homework/HW01')
Air <- read.csv('Airfares.csv')

#下面的函数现将变量从factor型变成字符型，接着去掉字符串当中所有的$和,符号，最后将变量变成数值型。
Factor2Num <- function(x)
{ AA <- as.character(x);
  pattern <- '[$,]+';
  AA <- as.numeric((str_replace_all(AA,pattern,'')));
  return (AA)                                             }

Air[,'S_INCOME'] <- Factor2Num (Air[,'S_INCOME']) #将S_INCOME变量从原来的factor型转换成数值型变量。
Air[,'E_INCOME'] <- Factor2Num (Air[,'E_INCOME'])
Air[,'FARE'] <- Factor2Num (Air[,'FARE'])

#(i)计算机票价格和数值型预测因子的相关系数，指出哪个数值型预测因子和机票价格相关性最大？
#以该预测因子为横轴，画出该因子与机票价格的散点图。

length(sapply(Air,is.numeric))         #air总共有多少列是numeric
numcols<-sapply(Air,is.numeric)[-18]   #除去FARE自己的筛选向量
cor(Air$FARE,Air[,numcols])            #计算相关系数 
qplot(DISTANCE,FARE,data=Air)          #距离与价格散点图

#(ii)探索机票价格和4个类别型变量VACATION、SW、SLOT、GATE的关系：
#计算每个类别型变量中，机票数据在该类别型变量表示的各类别中各有几条，
#在各个类别中的平均机票价格是多少。指出哪个类别型变量对机票价格的预测最有用。

table(Air$VACATION)   #no=468 yes=170
table(Air$SW)         #no=444 yes=194
table(Air$SLOT)       #controlled=182 free=456
table(Air$GATE)       #constrained=124 free=514

aggregate(FARE~VACATION, data=Air, mean) 
aggregate(FARE~SW, data=Air, mean)
aggregate(FARE~SLOT, data=Air, mean)
aggregate(FARE~GATE, data=Air, mean)     #可见SW造成的平均价格变动最大，应该在预测中最有用


#(iii)使用预测因子COUPON、NEW、HI、S_INCOME、E_INCOME、S_POP、E_POP、DISTANCE、PAX、
#SW、VACATION建立线性模型，并用机票价格FARE做为被解释变量，对所有数据进行回归分析。
#指出哪些变量在1%的显著性水平下显著。针对显著变量解释回归分析结果：该变量如何影响机票价格？

myfit <- lm(FARE~COUPON+NEW+HI+S_INCOME+E_INCOME+S_POP+E_POP+DISTANCE+PAX+SW+VACATION,
            data=Air)
summary(myfit)               
#1%的显著性水平下，HI、S_INCOME、E_INCOME、S_POP、E_POP、DISTANCE、PAX、SW、VACATION显著
#HI 市场集中度每增加1，机票均价大约上升0.087刀
#S_INCOME 起飞城市的人均收入每增加1，机票均价大约上升0.016刀
#E_INCOME 终点城市的人均收入每增加1，机票均价大约上升0.015刀
#S_POP  起飞城市的人口每增加1，机票均价大约上升4.452e-06刀
#E_POP  终点城市的人口每增加1，机票均价大约上升5.749e-06刀
#DISTANCE 飞行距离每增加1，机票均价大约上升0.071刀
#PAX  该航线的乘客数量每增加1，机票均价大约下降9.028e-04刀
#SW   西南航空运营该航线，则机票均价大约下降46.68刀
#VACATION 是旅游航线，则机票均价大约下降35.59刀


#(iv) 将（iii）当中用的模型做为full model，使用前向选择法（forward selection）选择变量。
# 写出最终的模型，并使用最终模型进行线性回归分析得到各个变量的系数。

min_model = lm (FARE ~ 1, data = Air)
full_model<-formula(myfit)
stepf <- stepAIC(min_model, direction="forward",scope=full_model)
stepf$coefficients
summary(stepf)

#最终模型FARE ~ DISTANCE + SW + VACATION + HI + E_POP + S_POP + PAX + E_INCOME + S_INCOME
#系数    (Intercept)      DISTANCE         SWYes   VACATIONYes            HI         E_POP         S_POP 
#      -3.419061e+01  7.257997e-02 -4.687025e+01 -3.560884e+01  8.178041e-03  5.792902e-06  4.445012e-06 
#                PAX      E_INCOME      S_INCOME 
#      -9.445595e-04  1.470679e-03  1.595628e-03 

#（v）将数据进行随机分组， 70%用于训练，30%用于验证，使用的线性模型是（iv）得到的最终模型，
# 随机分组前运行set.seed(1000)。分别计算训练集和验证集得到的RMS Error，验证集得到的RMS Error
# 是否明显大于训练集的RMS Error？最后画出验证集得到的残差的Box Plot。

set.seed(1000)
SampleIndex <- sample(1:nrow(Air),round(nrow(Air)*0.7),replace = FALSE)
AirSample<-Air[SampleIndex,]
fit_training <- lm(FARE ~ DISTANCE + SW + VACATION + HI + E_POP + S_POP + PAX + E_INCOME + S_INCOME
                   , data = AirSample)
summary(fit_training)
sum(fit_training$residuals^2)  
sqrt(sum(fit_training$residuals^2)/nrow(AirSample))   #训练RMS Error=36.99157

AirValidation<- Air[-SampleIndex,]
validation_result = predict(fit_training,newdata=AirValidation)
Vresiduals = AirValidation[,"FARE"]-validation_result
sum(Vresiduals^2)
sqrt(sum(Vresiduals^2)/length(Vresiduals))      #验证RMS Error=34.56452 并不显著大于训练RMS Error
boxplot(Vresiduals)
