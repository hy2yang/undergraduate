library(rpart)
library(rpart.plot) 
library(rattle)

setwd("D:/BA/Homework/HW03")
seg<-read.csv('Segmentation.csv',header = TRUE)

mymodel<-rpart(Class ~ ., data = seg,control = rpart.control(minsplit = 100))
summary(mymodel)
plot(mymodel)
rpart.plot(mymodel)
asRules(mymodel)
