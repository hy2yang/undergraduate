library(arules)

setwd("D:/BA/Homework/HW03")
Cosmetics<-read.csv('Cosmetics.csv',header = TRUE)

rules1 <- apriori(Cosmetics[,-1],
                 parameter = list(minlen=2,supp=0.1,conf=0.5),
                 appearance =list(lhs='Nail.Polish=yes',default="rhs"))
inspect(rules1)

rules2 <- apriori(Cosmetics[,-1],
                  parameter = list(minlen=2,supp=0.2,conf=0.5),
                  appearance =list(lhs='Mascara=yes',default="rhs"))
inspect(rules2)
