module Lists


//01
let FindLastElement01 lst =
    let rec FindLastElementRec lst =
        match lst with
        | [] -> None
        | [x] -> Some x
        | _::xs -> FindLastElementRec xs

    FindLastElementRec lst

let FindLastElement02 lst = List.reduce(fun _ x -> x) lst

//02
let FindLastTwoElements01 lst = 
    let rec FindLastTwoElementsRec lst = 
        match lst with
        | [] -> None
        | [first;second] -> Some (first,second)
        | _::xs -> FindLastTwoElementsRec xs

    FindLastTwoElementsRec lst