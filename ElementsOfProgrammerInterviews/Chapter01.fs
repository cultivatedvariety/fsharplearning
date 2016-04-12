module Chapter01

let ComputeParity num = 
    let rec ComputeParityRec num parity = 
        match num with
        | 0 -> (parity &&& 1)
        | _ -> ComputeParityRec (num >>> 1) (parity + (num &&& 1))

    ComputeParityRec num 0

