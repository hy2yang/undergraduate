library(ggplot2)
library(plyr)
library(RColorBrewer)
library(Hmisc) 
library(dplyr) 

setwd("D:/BA/Homework/HW04")

Sales <-read.csv("ProvSales.csv") 
CHNmapdata<-read.csv("ProvMapData.csv")
ProvIndex<-read.csv("ProvIndex.csv")

Sales_Index <-merge(Sales,ProvIndex,by="ProvNames")
Sales_Index <-arrange(Sales_Index,ProvID)

Sales_Map <- merge(CHNmapdata,Sales_Index,by.x="id",by.y="ProvID")
Sales_Map <- arrange(Sales_Map,id,order) 
head(Sales_Map)

Sales_Map$SScale<- cut(Sales_Map$ProvSales,breaks=c(0,3000,6000,12000,22000,26000,32000,42000))
mymap <- ggplot(Sales_Map, aes(x=long, y=lat, group=id, fill=SScale))+geom_polygon(colour="black")+coord_map()
mymap+scale_fill_brewer(palette="Greens")

