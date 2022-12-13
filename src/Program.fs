module AdventOfCode2022.Runner

open System.IO
open AdventOfCode2022.Common

type RunnerDef = { InputFile: string; Part1Handler: PuzzleHandler; Part2Handler: PuzzleHandler }

let runnerDefs = [|
    { InputFile = "Day1/input.txt"; Part1Handler = Day1.Part1.maxCalories; Part2Handler = Day1.Part2.maxCalories 3 }
    { InputFile = "Day2/input.txt"; Part1Handler = Day2.Part1.totalScore; Part2Handler = Day2.Part2.totalScore }
    { InputFile = "Day3/input.txt"; Part1Handler = Day3.Part1.totalPriorities; Part2Handler = Day3.Part2.totalPriorities }
    { InputFile = "Day4/input.txt"; Part1Handler = Day4.Part1.totalInclusiveAssignments; Part2Handler = Day4.Part2.totalOverlappingAssignments }
    { InputFile = "Day5/input.txt"; Part1Handler = Day5.Part1.getTopStacks; Part2Handler = Day5.Part2.getTopStacks }
    { InputFile = "Day6/input.txt"; Part1Handler = Day6.Part1.startOfPacketLastCharIndex; Part2Handler = Day6.Part2.startOfMessageLastCharIndex }
|]

let runDayDef dayIndex {InputFile = inputFile; Part1Handler = part1Handler; Part2Handler = part2Handler } =
    printfn $"Executing Day #%i{dayIndex + 1}"
    File.ReadLines inputFile |> part1Handler |> stringifyResult |> printfn "\tPart1: %s"
    File.ReadLines inputFile |> part2Handler |> stringifyResult |> printfn "\tPart2: %s"
    printfn ""
    
Array.iteri runDayDef runnerDefs
    