library(fpp)

data(euretail)

mult_fit <- decompose(euretail, type="multiplicative")
plot(mult_fit)

add_fit <- decompose(euretail, type="additive")
plot(add_fit)


fit1<-hw(euretail, seasonal="additive")
fit2<-hw(euretail, seasonal="multiplicative")

plot(euretail,main='Quaterly Retail Trade Index in the Euro Area',ylab="Retail Index",
     plot.conf=FALSE, type="o", fcol="white", xlab="Year",xlim=c(1995,2015))
lines(fitted(fit1), col="red", lty=2)
lines(fitted(fit2), col="green", lty=2)
lines(fit1$mean, type="o", col="red")
lines(fit2$mean, type="o", col="green")
legend("topleft",lty=1, pch=1,cex=0.5, col=1:3,
       c("data","Holt Winters' Additive","Holt Winters'
Multiplicative"))
