select author1,author2,author3,count(concat(author1,author2,author3)) as cnt 
into FrequentTri
from TriAuthors 
group by author1,author2,author3
having count(concat(author1,author2,author3))>2
