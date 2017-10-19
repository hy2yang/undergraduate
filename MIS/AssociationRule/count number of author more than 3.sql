select title, count(title) as cnt
from SouceData
group by title
having count(title)>2