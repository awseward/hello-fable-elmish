// Learn more about F# at http://fsharp.org

open System
open Elmish

module Counter =
  type Model = { count: int }

  type Msg =
  | Increment
  | Decrement

  let init() =
    { count = 0 }, Cmd.ofMsg Increment

  let update msg model =
    match msg with
    | Increment ->
        { model with count = model.count + 1 }, Cmd.ofMsg Decrement
    | Decrement ->
        { model with count = model.count - 1 }, Cmd.ofMsg Increment

type Model =
  { top : Counter.Model
    bottom : Counter.Model }

type Msg =
| Reset
| Top of Counter.Msg
| Bottom of Counter.Msg

let init () =
  let top, topCmd = Counter.init()
  let bottom, bottomCmd = Counter.init()

  { top = top
    bottom = bottom },
  Cmd.batch [ Cmd.map Top topCmd
              Cmd.map Bottom bottomCmd ]

let update msg model : Model * Cmd<Msg> =
  match msg with
  | Reset -> init()
  | Top msg' ->
      let res, cmd = Counter.update msg' model.top
      { model with top = res }, Cmd.map Top cmd

  | Bottom msg' ->
      let res, cmd = Counter.update msg' model.bottom
      { model with bottom = res }, Cmd.map Bottom cmd


[<EntryPoint>]
let main argv =
  Program.mkProgram init update (fun model _ -> printf "%A\n" model)
  |> Program.run
  |> ignore

  Console.ReadLine()

  0