select SouceData.title,FrequentTri.author1,FrequentTri.author2,FrequentTri.author3
from soucedata,FrequentTri
where SouceData.author=FrequentTri.author1 
or SouceData.author=FrequentTri.author2
or SouceData.author=FrequentTri.author3
group by FrequentTri.author1,FrequentTri.author2,FrequentTri.author3,SouceData.title