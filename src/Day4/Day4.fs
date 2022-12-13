module AdventOfCode2022.Day4

open AdventOfCode2022.Common
        
type RangeAssignment = RangeAssignment of int * int
type SortedRangeAssignments = SortedRangeAssignments of RangeAssignment * RangeAssignment

let private parseAssignment (assignment:string) =
    let rangeEntries = assignment.Split('-')
    RangeAssignment (int rangeEntries[0], int rangeEntries[1])
    
let private parseAssignmentPair (line:string) =
    let assignments = line.Split(',') |> Array.map parseAssignment |> Array.sort
    SortedRangeAssignments (assignments[0], assignments[1])
        
module Part1 =
        
    let private includesAssignments (RangeAssignment (outerLowerBound, outerUpperBound))  (RangeAssignment (innerLowerBound, innerUpperBound))=
        (outerLowerBound <= innerLowerBound && outerUpperBound >= innerUpperBound)
        
    let private isInclusiveAssignments (SortedRangeAssignments (a1, a2)) =
        includesAssignments a1 a2 || includesAssignments a2 a1
        
    let totalInclusiveAssignments : PuzzleHandler =
        Seq.map parseAssignmentPair
        >> Seq.filter isInclusiveAssignments
        >> Seq.length
        >> IntegerResult
        
module Part2 =
    
    let private overlapsAssignments (RangeAssignment (_, x2))  (RangeAssignment (y1, _)) =
        x2 >= y1
        
    let private isOverlappingAssignments (SortedRangeAssignments (a1, a2)) =
        overlapsAssignments a1 a2
        
    let totalOverlappingAssignments : PuzzleHandler =
        Seq.map parseAssignmentPair
        >> Seq.filter isOverlappingAssignments
        >> Seq.length
        >> IntegerResult