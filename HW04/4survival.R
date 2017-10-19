library(survival)
library(OIsurv)

setwd("D:/BA/Homework/HW04")

hiv<-read.csv('Hmohiv.csv',header=TRUE)
attach(hiv)

my.surv<-Surv(time,status)
my.fit<-survfit(my.surv~1)   
summary(my.fit)
plot(my.fit)

survdiff(Surv(time, status) ~ drug)

detach(hiv)
