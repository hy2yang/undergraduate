select author1,author2,author3,count(concat(author1,author2,author3)) as cnt
into TriFrequency
from TriAuthors
group by author1,author2,author3