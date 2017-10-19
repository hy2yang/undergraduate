library(class)
library(plyr) #needed for arrange
library(e1071) ## needed for Naive Bayes
setwd("D:/BA/Homework/HW02")
Accidents <- read.csv("Accidents.csv",header = TRUE)

#将MAX_SEV_IR等于1或者2的值全部设置为1。
Accidents[Accidents[,'MAX_SEV_IR']>0,'MAX_SEV_IR'] = 1
#将1-13,15-17,19-20,22-24列转换成类别型变量。
AA <- c(1:13,15:17,19:20,22:24);
for (i in 1:length(AA)) {
  Accidents[,AA[i]] <- factor(Accidents[,AA[i]])
}

#Q1.1
#取出Accidents数据集的子集前12条记录，只取WEATHER_R和TRAF_CON_R为预测变量。
AccSub <- Accidents[c(1:12),c('WEATHER_R','TRAF_CON_R','MAX_SEV_IR')]
AccSub <- data.frame(AccSub,COUNT=rep(1,nrow(AccSub)))
#下面列出分类汇总表：

AccSubAg<-aggregate(AccSub$COUNT,by=list(WEATHER_R=AccSub[,1],TRAF_CON_R=AccSub[,2],MAX_SEV_IR=AccSub[,3]),FUN=sum)
arrange(AccSubAg,WEATHER_R,TRAF_CON_R)

#Q1.2 
   'P(MAX_SEV_IR=1 | (WEATHER_R=2,TRAF_CON_R=0))=1/(5+1)=1/6'

#Q1.3
attach(AccSub)
table(MAX_SEV_IR,WEATHER_R)
table(MAX_SEV_IR,TRAF_CON_R)
table(MAX_SEV_IR)
detach(AccSub)

  'P(MAX_SEV_IR=1 | (WEATHER_R=2,TRAF_CON_R=0))=(1/4 * 1 * 1/3)/((1/4 * 1 * 1/3)+(3/4 * 2/3 * 2/3))=1/5'

#Q1.4: 使用Naive Bayes模型
AccSub[2,]
Predictors <- AccSub[,c('WEATHER_R','TRAF_CON_R')]
Target <- AccSub[,'MAX_SEV_IR']
classifier<-naiveBayes(Predictors, Target) 
Probs <- predict(classifier, Predictors, type='raw',threshold=0.01)
Probs
#在AccSub中新增加一列，列名为PredictInj，初始值为0,1,0,1...
AccSub <- data.frame(AccSub,PredictInj = rep(0:1,6))

(AccSub[,'PredictInj']<-1*(Probs[,2]>0.4))
table(AccSub[,c(3,5)])

#Q1.5
AccidentModel <- Accidents[,c('HOUR_I_R','ALIGN_I','WRK_ZONE','WKDY_I_R','INT_HWY','RELJCT_I_R',
                              'REL_RWY_R','TRAF_CON_R','TRAF_WAY','MAX_SEV_IR')]
#以HOUR_I_R,ALIGN_I...'MAX_SEV_IR'建立模型
set.seed(1000)
#固定随机数种子为1000
RowNum <- nrow(AccidentModel)
#取模型中行数
SampleIndex <- sample(1:RowNum,round(RowNum*0.8),replace = FALSE)
#前80%的行索引不放回取出作为样本
TrainData <- AccidentModel[SampleIndex,] 
#样本定义为训练集
ValidationData <- AccidentModel[-SampleIndex,] 
#后20%定义为验证集
TargetIndex <- which(colnames(AccidentModel)=='MAX_SEV_IR')  
#预测目标为名为MAX_SEV_IR的列
Predictors <- TrainData[,-TargetIndex]
#其余为预测因子


#Q1.6
#注意下面的空白处是由你来填写代码，而不是让你通过注释回答问题。
#对问题的所有回答都应该写在Word文档里面。
#不过你可以写注释帮助TA理解你写的代码是什么意思，以便你在出错时TA可以找到理由给你部分分数。

classifier<-naiveBayes(Predictors,TrainData[,TargetIndex])

MyPredict<-predict(classifier, ValidationData[,-TargetIndex])

table(MyPredict,ValidationData[,TargetIndex])

#Q1.7

blind<-naiveBayes(TrainData[,TargetIndex],TrainData[,TargetIndex])
blindP<- predict(blind, ValidationData[,TargetIndex])
table(blindP,ValidationData[,TargetIndex])
#全预测为1

#Q1.8
4176/(4176+4261)
(1683+2265)/(1683+2265+1911+2578)
#朴素贝叶斯分类器错误率略小

#Q1.9

rawP<-data.frame((predict(classifier, ValidationData[,-TargetIndex],type ='raw')))
myTable <- data.frame(cutoff=NULL,Loss=NULL)

for (i in seq(round(min(rawP$X1),digits = 2),round(max(rawP$X1),digits = 2),0.001)) {
 
  mypred <- data.frame(pred =ifelse(rawP[,'X1']>i,1,0))
  
  myLoss <- sum(mypred[,'pred']!=ValidationData[,'MAX_SEV_IR'] & ValidationData[,'MAX_SEV_IR']=='0')*1.2+
    sum(mypred[,'pred']!=ValidationData[,'MAX_SEV_IR'] & ValidationData[,'MAX_SEV_IR']=='1')
  
  myTable <- rbind(myTable,data.frame(cutoff=i,Loss=myLoss))
  
}
View(myTable)

library(ggplot2)
qplot(cutoff,Loss,data=myTable,geom ="line")
idx <- which.min(myTable[,"Loss"])
myTable[idx,]

(optcutoff <- myTable[idx,"cutoff"])
mypred <- data.frame(pred =ifelse(rawP[,'X1']>optcutoff,1,0),
                     obs = ValidationData[,'MAX_SEV_IR'])
table(mypred)
(911+3127)/(911+3127+3265+1134)
