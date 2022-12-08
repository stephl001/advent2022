open System.IO

let folder (sum,allSums) = function
| "" -> (0, sum::allSums)
| cal -> (sum + int cal, allSums)

let maxCalories n =
    File.ReadLines
    >> Seq.fold folder (0,[])
    >> snd
    >> List.sortDescending
    >> List.take n
    >> List.sum

let args : string array = fsi.CommandLineArgs |> Array.tail
maxCalories 3 args[0] |> printfn "%i"