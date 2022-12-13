module AdventOfCode2022.Day6

open AdventOfCode2022.Common

let distinctIndex size =
    Seq.windowed size
    >> Seq.map Array.distinct
    >> Seq.indexed
    >> Seq.find (fun (_,arr) -> arr.Length = size)
    >> fst
    
let distinctLastCharIndex size =
    distinctIndex size >> (+) size

module Part1 =

    let startOfPacketLastCharIndex : PuzzleHandler =
        Seq.head >> distinctLastCharIndex 4 >> IntegerResult
        
module Part2 =

    let startOfMessageLastCharIndex : PuzzleHandler =
        Seq.head >> distinctLastCharIndex 14 >> IntegerResult 