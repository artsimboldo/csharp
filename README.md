#CSharp stuffs

##TestJson
TestJson benchmarks .Net Json libraries:  
1. fastJSON  
2. ServiceStack  
3. LitJson  
4. MongoDB Bson  
5. JSON.NET  

#### Benchmark 1 : 1,000 Dictionary<> deserialization (*):
| Rank   | Library       | Elapsed time  |
| -------|:-------------:| -------------:|
| 1      | fastJSON      | 56ms |
| 2      | ServiceStack  | 97ms |
| 3      | LitJson       | 110ms |
| 4      | MongoDB Bson  | 170ms |
| 5      | JSON.NET      | 207ms |
(*): Intel Core i7-4720HQ CPU @ 2.60GHs  
