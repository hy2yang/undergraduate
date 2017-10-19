select author1,author2,count(concat(author1,author2)) as cnt
into DualFrequency
from DualAuthors
group by author1,author2