select author1 as author
from DualFrequency
where cnt>5
union
select author2 as author 
from DualFrequency
where cnt>5
