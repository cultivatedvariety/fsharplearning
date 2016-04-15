module Lists



let FindLastElement01 lst =
    let rec FindLastElementRec lst =
        match lst with
        | [] -> None
        | [x] -> Some x
        | _::xs -> FindLastElementRec xs

    FindLastElementRec lst

let FindLastElement02 lst = List.reduce(fun _ x -> x) lst
    