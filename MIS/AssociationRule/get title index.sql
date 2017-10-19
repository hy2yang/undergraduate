select title into dbo.TitleIndex
from dbo.SouceData
group by title
having count(title)>1
