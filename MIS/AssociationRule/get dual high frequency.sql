select author1,author2,count(concat(author1,author2)) as cnt 
into FrequentDual
from DualAuthors
group by author1,author2
having count(concat(author1,author2))>2