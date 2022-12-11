open System
open System.IO

let priority (item:char) =
    if Char.IsLower(item) 
    then int item - int 'a' + 1
    else int item - int 'A' + 27
    
let groupPriority =
    Array.map Set.ofSeq >> Array.reduce Set.intersect >> Set.toList >> List.sumBy priority
    
let totalPriorities =
    File.ReadLines >> Seq.chunkBySize 3 >> Seq.map groupPriority >> Seq.sum

let args : string array = fsi.CommandLineArgs |> Array.tail
totalPriorities args[0] |> printfn "%i"