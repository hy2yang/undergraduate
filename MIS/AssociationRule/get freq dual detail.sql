select SouceData.title,FrequentDual.author1,FrequentDual.author2
from soucedata,FrequentDual
where SouceData.author=FrequentDual.author1 
or SouceData.author=FrequentDual.author2
group by FrequentDual.author1,FrequentDual.author2,SouceData.title