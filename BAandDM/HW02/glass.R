
library(caret)
setwd("D:/BA/Homework/HW02")

fgl <- read.csv("fgl.csv",header = TRUE)

Predictors <- fgl[,-10]

model <- train(Predictors, fgl[,10],method='knn',
  tuneGrid = data.frame(k=1:8),
  metric='Accuracy',
  trControl=trainControl(method='repeatedcv', number=5, repeats=20)
  )
plot(model)
confusionMatrix(model)

#将变量分段后转化为类别型变量