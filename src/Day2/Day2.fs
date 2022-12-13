module AdventOfCode2022.Day2

open AdventOfCode2022.Common

type Shapes = Rock | Paper | Scissors
type Round = Shapes * Shapes

module Part1 =

    let private shapesFromCode = function
        | "A" | "X" -> Rock
        | "B" | "Y" -> Paper
        | "C" | "Z" -> Scissors
        | _ -> failwith "Invalid input"
        
    let private scoreRound = function
        | (Rock, Paper) -> 2 + 6
        | (Rock, Scissors) -> 3 + 0
        | (Paper, Rock) -> 1 + 0
        | (Paper, Scissors) -> 3 + 6
        | (Scissors, Rock) -> 1 + 6
        | (Scissors, Paper) -> 2 + 0
        | (Rock, _) -> 1 + 3
        | (Paper, _) -> 2 + 3
        | (Scissors, _) -> 3 + 3
        
    let private parseRound (text:string) =
        let shapes = text.Split(' ') |> Array.map shapesFromCode
        (shapes[0], shapes[1])
        
    let totalScore : PuzzleHandler =
        Seq.map (parseRound >> scoreRound) >> Seq.sum >> IntegerResult
        
module Part2 =
    
    let private shapeFromOutcome p1Shape outcome =
        match (p1Shape, outcome) with
        | (s, "Y") -> s
        | (Rock, "X") -> Scissors
        | (Rock, "Z") -> Paper
        | (Paper, "X") -> Rock
        | (Paper, "Z") -> Scissors
        | (Scissors, "X") -> Paper
        | (Scissors, "Z") -> Rock
        | _ -> failwith "invalid input"
        
    let private shapesFromCode = function
        | ("A", outcome) -> (Rock, shapeFromOutcome Rock outcome)
        | ("B", outcome) -> (Paper, shapeFromOutcome Paper outcome)
        | ("C", outcome) -> (Scissors, shapeFromOutcome Scissors outcome)
        | _ -> failwith "Invalid input"
        
    let private scoreRound = function
        | (Rock, Paper) -> 2 + 6
        | (Rock, Scissors) -> 3 + 0
        | (Paper, Rock) -> 1 + 0
        | (Paper, Scissors) -> 3 + 6
        | (Scissors, Rock) -> 1 + 6
        | (Scissors, Paper) -> 2 + 0
        | (Rock, _) -> 1 + 3
        | (Paper, _) -> 2 + 3
        | (Scissors, _) -> 3 + 3
        
    let private parseRound (text:string) =
        let shapes = text.Split(' ')
        shapesFromCode (shapes[0], shapes[1])
        
    let totalScore : PuzzleHandler =
        Seq.map (parseRound >> scoreRound) >> Seq.sum >> IntegerResult

