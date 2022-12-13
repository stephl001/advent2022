module AdventOfCode2022.Day5

open System
open AdventOfCode2022.Common
open System.Collections.Generic
open FParsec

type Crate = Crate of char
type CrateStack = CrateStack of Stack<Crate>

type Command =
    { Count: int
      SourceStack: int
      TargetStack: int }

type StackDefinition =
    | Empty
    | CrateDef of char

let private crateFromDef =
    function
    | CrateDef c -> Crate c
    | Empty -> failwith "Unexpected"

let private charFromCrate (Crate c) = c

let private emptyCrateDefP = pstring "   " >>% Empty

let private crateDefP =
    skipChar '[' >>. satisfy Char.IsLetter .>> skipChar ']'
    |>> (Char.ToUpper >> CrateDef)

let private stackDefLineP = sepBy (emptyCrateDefP <|> crateDefP) (pchar ' ')

let private commandP: Parser<Command, unit> =
    skipString "move " >>. pint32 .>> skipString " from " .>>. pint32
    .>> skipString " to "
    .>>. pint32
    |>> (fun ((c, s), t) ->
        { Count = c
          SourceStack = s
          TargetStack = t })

let private parseDefLine line =
    match run stackDefLineP line with
    | Success (stackDefinitions, _, _) -> stackDefinitions
    | _ -> failwith "Unexpected error"

let private parseCommand line =
    match run commandP line with
    | Success (cmd, _, _) -> cmd
    | _ -> failwith "Unexpected error"

let private isStackIdLine (line: string) = line.StartsWith(" 1")

let private isCommand (line: string) = line.StartsWith("move")

let rec private transpose xs =
    [ match xs with
      | [] -> failwith "cannot transpose a 0-by-n matrix"
      | [] :: xs -> ()
      | xs ->
          yield List.map List.head xs
          yield! transpose (List.map List.tail xs) ]

let private readCrateStacks (lines: string seq) =
    lines
    |> Seq.takeWhile (not << isStackIdLine)
    |> Seq.map parseDefLine
    |> Seq.toList
    |> transpose
    |> List.map (List.filter ((<>) Empty) >> List.map crateFromDef)
    |> List.toArray

let private readCommands (lines: string seq) =
    lines |> Seq.skipWhile (not << isCommand) |> Seq.map parseCommand

let private popList =
    function
    | [] -> []
    | _ :: xs -> xs
  
let private runSimulation moveCrateFn lines =
        let cachedLines = Seq.cache lines
        let crateStacks = readCrateStacks cachedLines

        readCommands cachedLines
        |> Seq.fold moveCrateFn crateStacks
        |> Array.map (List.head >> charFromCrate)
        |> String
        |> TextResult

module Part1 =

    let private moveCrate (crateStacks: Crate list array) (sourceIndex, targetIndex) =
        let crate = List.head crateStacks[sourceIndex - 1]
        crateStacks[sourceIndex - 1] <- popList crateStacks[sourceIndex - 1]
        crateStacks[targetIndex - 1] <- crate :: crateStacks[targetIndex - 1]
        crateStacks
        
    let private moveCrates (crateStacks: Crate list array) command =
        List.init command.Count (fun _ -> (command.SourceStack, command.TargetStack))
        |> List.fold moveCrate crateStacks

    let getTopStacks : PuzzleHandler =
        runSimulation moveCrates
        
module Part2 =

    let private moveCrates (crateStacks: Crate list array) {Count=count; SourceStack=sourceStack; TargetStack=targetStack} =
        crateStacks[targetStack-1] <- List.take count crateStacks[sourceStack-1] @ crateStacks[targetStack-1]
        crateStacks[sourceStack-1] <- List.skip count crateStacks[sourceStack-1]
        crateStacks        

    let getTopStacks : PuzzleHandler =
        runSimulation moveCrates