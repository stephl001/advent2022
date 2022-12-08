open System.IO

let folder (sum,maxSum) = function
| "" -> (0, max sum maxSum)
| cal -> (sum + int cal, maxSum)

let maxCalories inputFile =
    File.ReadLines inputFile
    |> Seq.fold folder (0,0)
    |> snd

let args : string array = fsi.CommandLineArgs |> Array.tail
maxCalories args[0] |> printfn "%i"