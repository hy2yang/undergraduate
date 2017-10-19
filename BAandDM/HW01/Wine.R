setwd('D:/BA/Homework/HW01')
wine <- read.csv('Wine.csv')
summary(wine)

pca_noscale<- prcomp(wine[,2:14],scale = FALSE)
print(pca_noscale)
summary(pca_noscale)  #一个主成分变量可捕捉到99.81%的总方差

pca_scale<- prcomp(wine[,2:14],scale = TRUE)
print(pca_scale)
summary(pca_scale)   #八个主成分变量可捕捉到92.02%的总方差

#标准化处理更好，由非标准化分析结果可得Proline一个因素几乎完全构成第一个主成分
#因为其量级比较大，所以权重很高，对其它指标不公平