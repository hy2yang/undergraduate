select B.author1 as a1,A.author1 as a2,A.author2 as a3
from DualFrequency A,DualFrequency B
where A.cnt>2 and B.cnt>2 and (A.author1=B.author2 or A.author2=B.author2)
      and not(A.author1=B.author1 and A.author2=B.author2)   
union
select A.author1 as a1,A.author2 as a2,B.author2 as a3
from DualFrequency A,DualFrequency B
where A.cnt>2 and B.cnt>2 and (A.author2=B.author1 or A.author1=B.author1)
      and not(A.author1=B.author1 and A.author2=B.author2)
union
select A.author1 as a1,B.author2 as a2,A.author2 as a3
from DualFrequency A,DualFrequency B
where A.cnt>2 and B.cnt>2 and A.author1=B.author1
     and not(A.author1=B.author1 and A.author2=B.author2)
union
select A.author1 as a1,B.author1 as a2,A.author2 as a3
from DualFrequency A,DualFrequency B
where A.cnt>2 and B.cnt>2 and A.author2=B.author2
     and not(A.author1=B.author1 and A.author2=B.author2)
