library(XML)

url = 'http://www.thepaper.cn/' #澎湃新闻的URL。
doc = htmlParse(url)  #提取网页源代码

#提取一天之内的热门新闻
#例如从附件《澎湃新闻》PDF文件提取出的结果是：
#[1] "女网选手张帅怒斥美联航：工作人员强抢网球包，登机牌被撕破"
#[2] "国务院严厉问责西安地铁问题电缆事件：122名责任人被处理"  
#......
#[10] "江西省出席党的十九大代表名单公布，共43人"
xpathSApply(doc, "//*[@id='listhot0']/li/a",xmlValue)

#提取三天之内的热门新闻：
xpathSApply(doc, "//*[@id='listhot1']/li/a",xmlValue)

#提取一周之内的热门新闻：
xpathSApply(doc, "//*[@id='listhot2']/li/a",xmlValue)

url = 'http://www.jiemian.com/lists/32.html' #界面新闻天下栏目的URL。
doc = htmlParse(url)  #提取网页源代码

#提取界面新闻天下栏目首页中《最新报道》下所有新闻的标题，
#不需要提取《突发》、《图片报道》和《最热》下的新闻标题。
#例如从附件PDF展示的页面中提取出来的标题有20条：
# [1] "特朗普儿子力挺父亲 狠批民主党议员“不是人”"                         
# [2] "OECD：全球经济将转好 但仍不够好"                                     
# [3] "南非遭遇30年来最强风暴 1万人撤离总统专机无法起飞"                    
# ......
# [19] "【研究】洗手该用温水还是凉水？"                                     
# [20] "40年来最重大修订 世卫组织首次提出抗生素分类标准" 
xpathSApply(doc, "//*[@id='load-list']//h3/a",xmlValue)


#提取这些新闻的URL链接：
#例如从附件PDF展示的页面中提取出来的URL有20条：
# [1] "http://www.jiemian.com/article/1382081.html" "http://www.jiemian.com/article/1382016.html"
# [3] "http://www.jiemian.com/article/1382071.html" "http://www.jiemian.com/article/1381900.html"
# ......
# [19] "http://www.jiemian.com/article/1378595.html" "http://www.jiemian.com/article/1379064.html"
xpathSApply(doc, "//*[@id='load-list']//h3/a",xmlGetAttr, "href")

