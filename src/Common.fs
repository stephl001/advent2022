module AdventOfCode2022.Common

type PuzzleResult =
    | IntegerResult of int
    | TextResult of string
    
type PuzzleHandler = string seq -> PuzzleResult

let stringifyResult = function
    | IntegerResult i -> string i
    | TextResult s -> s

