open System
open System.IO

let priority (item:char) =
    if Char.IsLower(item) 
    then int item - int 'a' + 1
    else int item - int 'A' + 27
    
let splitContents (contents:string) =
    contents.Substring(0, contents.Length / 2), contents.Substring(contents.Length / 2)
    
let getSharedItems (sack1, sack2) =
    Set.intersect (Set.ofSeq sack1) (Set.ofSeq sack2) |> Set.toList
    
let getSharedPriorities =
    splitContents >> getSharedItems >> List.sumBy priority
    
let totalPriorities =
    File.ReadLines >> Seq.sumBy getSharedPriorities

let args : string array = fsi.CommandLineArgs |> Array.tail
totalPriorities args[0] |> printfn "%i"