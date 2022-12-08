open System.IO

type Shapes = Rock | Paper | Scissors
type Round = Shapes * Shapes

let shapesFromCode = function
    | "A" | "X" -> Rock
    | "B" | "Y" -> Paper
    | "C" | "Z" -> Scissors
    | _ -> failwith "Invalid input"
    
let scoreRound = function
    | (Rock, Paper) -> 2 + 6
    | (Rock, Scissors) -> 3 + 0
    | (Paper, Rock) -> 1 + 0
    | (Paper, Scissors) -> 3 + 6
    | (Scissors, Rock) -> 1 + 6
    | (Scissors, Paper) -> 2 + 0
    | (Rock, _) -> 1 + 3
    | (Paper, _) -> 2 + 3
    | (Scissors, _) -> 3 + 3
    
let parseRound (text:string) =
    let shapes = text.Split(' ') |> Array.map shapesFromCode
    (shapes[0], shapes[1])
    
let totalScore =
    File.ReadLines >> Seq.map (parseRound >> scoreRound) >> Seq.sum
    
let args : string array = fsi.CommandLineArgs |> Array.tail
totalScore args[0] |> printfn "%i"