library(cluster) #needed for hierachical clustering
library(plyr) #needed for arrange
library(ggplot2) #needed for qplot

setwd("D:/BA/Homework/HW03")
EastWestData <- read.csv("EastWestAirlines.csv",header = TRUE)
set.seed(1200) #设置随机种子
EastWest <- EastWestData[sample(nrow(EastWestData),1000),] #随机从数据框EastWestData中抽出1000个数据进行聚类。
EastWestModel <- EastWest[,  c('Balance','Qual_miles','Bonus_miles','Bonus_trans',
                               'Flight_miles_12mo','Flight_trans_12')]

#聚类距离指标：average
EastWestAgg = agnes(EastWestModel,diss=FALSE,metric="euclidian",method="average",stand=TRUE)
#把树图割成20个clusters。stand=TRUE意味着聚类之前先对数据进行标准化处理。
treeidx <- cutree(EastWestAgg, k=20)
#在各行数据上打上标签说明该行数据是第几个cluster，标签存在Class列。
EastWestClusters <- data.frame(EastWest, Class=treeidx) 
#将Class列转换成factor型变量。
EastWestClusters$Class <- as.factor(EastWestClusters$Class)

aggregate(Balance ~ Class, data=EastWestClusters, length) #
aggregate(. ~ Class, data=EastWestClusters, mean) #

EastWestClusters <- data.frame(EastWest, Class=2-(treeidx==1)) #
EastWestClusters$Class <- as.factor(EastWestClusters$Class) #将Class列转换成factor型变量。
aggregate(Balance ~ Class, data=EastWestClusters, length) 
aggregate(. ~ Class, data=EastWestClusters, mean) 

qplot(Balance,Qual_miles,data=EastWestClusters,colour = Class)
qplot(Bonus_trans,Bonus_miles,data=EastWestClusters,colour = Class)
qplot(Flight_trans_12,Flight_miles_12mo,data=EastWestClusters,colour = Class)

A1 <- arrange(aggregate(Balance ~ Class + as.factor(Award.), data=EastWestClusters, length), Class)
A1
A2 <- t(A1)
A2
AA <- rbind(as.numeric(A2[3,1:2]),as.numeric(A2[3,3:4]))
colnames(AA) <- c('no.Award','with.Award')
AA
labels <- c('no.Award','with.Award')
barplot(AA, offset = 0, axis.lty = 1, names.arg = labels, col = c('blue1','green2'))

#Q1.8
#聚类距离指标：ward
EastWestAgg = agnes(EastWestModel,diss=FALSE,metric="euclidian",method="ward",stand=TRUE)

treeidx2 <- cutree(EastWestAgg, k=4)
EastWestClusters2 <- data.frame(EastWest, Class=treeidx2) 
EastWestClusters2$Class <- as.factor(EastWestClusters2$Class)

aggregate(Balance ~ Class, data=EastWestClusters2, length) #
aggregate(. ~ Class, data=EastWestClusters2, mean) #

EastWestClusters2 <- data.frame(EastWest, Class=2-(treeidx==2)) #
EastWestClusters2$Class <- as.factor(EastWestClusters2$Class) 
qplot(Balance,Qual_miles,data=EastWestClusters2,colour = Class)
qplot(Bonus_trans,Bonus_miles,data=EastWestClusters2,colour = Class)
qplot(Flight_trans_12,Flight_miles_12mo,data=EastWestClusters2,colour = Class)
