open System.IO

type Shapes = Rock | Paper | Scissors
type Round = Shapes * Shapes

let shapeFromOutcome p1Shape outcome =
    match (p1Shape, outcome) with
    | (s, "Y") -> s
    | (Rock, "X") -> Scissors
    | (Rock, "Z") -> Paper
    | (Paper, "X") -> Rock
    | (Paper, "Z") -> Scissors
    | (Scissors, "X") -> Paper
    | (Scissors, "Z") -> Rock
    | _ -> failwith "invalid input"
    
let shapesFromCode = function
    | ("A", outcome) -> (Rock, shapeFromOutcome Rock outcome)
    | ("B", outcome) -> (Paper, shapeFromOutcome Paper outcome)
    | ("C", outcome) -> (Scissors, shapeFromOutcome Scissors outcome)
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
    let shapes = text.Split(' ')
    shapesFromCode (shapes[0], shapes[1])
    
let totalScore =
    File.ReadLines >> Seq.map (parseRound >> scoreRound) >> Seq.sum
    
let args : string array = fsi.CommandLineArgs |> Array.tail
totalScore args[0] |> printfn "%i"