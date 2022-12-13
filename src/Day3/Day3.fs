module AdventOfCode2022.Day3

open System
open AdventOfCode2022.Common

let private priority (item:char) =
    if Char.IsLower(item) 
    then int item - int 'a' + 1
    else int item - int 'A' + 27
        
module Part1 =
        
    let private splitContents (contents:string) =
        contents.Substring(0, contents.Length / 2), contents.Substring(contents.Length / 2)
        
    let private getSharedItems (sack1, sack2) =
        Set.intersect (Set.ofSeq sack1) (Set.ofSeq sack2) |> Set.toList
        
    let private getSharedPriorities =
        splitContents >> getSharedItems >> List.sumBy priority
        
    let totalPriorities : PuzzleHandler =
        Seq.sumBy getSharedPriorities >> IntegerResult
        
module Part2 =
           
    let groupPriority =
        Array.map Set.ofSeq >> Array.reduce Set.intersect >> Set.toList >> List.sumBy priority
        
    let totalPriorities : PuzzleHandler =
        Seq.chunkBySize 3 >> Seq.map groupPriority >> Seq.sum >> IntegerResult