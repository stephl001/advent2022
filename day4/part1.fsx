open System.IO

type RangeAssignment = RangeAssignment of int * int

let parseAssignment (assignment:string) =
    let rangeEntries = assignment.Split('-')
    RangeAssignment (int rangeEntries[0], int rangeEntries[1])
    
let parseAssignmentPair (line:string) =
    let assignments = line.Split(',') |> Array.map parseAssignment
    (assignments[0], assignments[1])
    
let includesAssignments (RangeAssignment (outerLowerBound, outerUpperBound))  (RangeAssignment (innerLowerBound, innerUpperBound))=
    (outerLowerBound <= innerLowerBound && outerUpperBound >= innerUpperBound)
    
let isInclusiveAssignments (a1, a2) =
    includesAssignments a1 a2 || includesAssignments a2 a1
    
let assignmentPairsFromFile =
    File.ReadLines
    >> Seq.map parseAssignmentPair
    >> Seq.filter isInclusiveAssignments
    >> Seq.length
    
let args : string array = fsi.CommandLineArgs |> Array.tail
assignmentPairsFromFile args[0] |> printfn "%i"

    