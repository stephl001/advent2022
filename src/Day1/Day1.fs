module AdventOfCode2022.Day1

open AdventOfCode2022.Common

module Part1 =

    let private folder (sum,maxSum) = function
    | "" -> (0, max sum maxSum)
    | cal -> (sum + int cal, maxSum)

    let maxCalories : PuzzleHandler =
        Seq.fold folder (0,0) >> snd >> IntegerResult
        
module Part2 =
    
    let private folder (sum,allSums) = function
    | "" -> (0, sum::allSums)
    | cal -> (sum + int cal, allSums)

    let maxCalories topElves : PuzzleHandler =
        Seq.fold folder (0,[])
        >> snd
        >> List.sortDescending
        >> List.take topElves
        >> List.sum
        >> IntegerResult