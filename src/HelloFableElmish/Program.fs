// Learn more about F# at http://fsharp.org

open System

type Model = { x: int }

type Msg =
| Increment
| Decrement

open Elmish

let init () =
  { x = 0}, Cmd.ofMsg Increment

let update msg model =
  match msg with
  | Increment when model.x < 3 ->
      { model with x = model.x + 1 }, Cmd.ofMsg Increment

  | Increment ->
      { model with x = model.x + 1 }, Cmd.ofMsg Decrement

  | Decrement when model.x > 0 ->
      { model with x = model.x - 1 }, Cmd.ofMsg Decrement

  | Decrement ->
      { model with x = model.x - 1 }, Cmd.ofMsg Increment

[<EntryPoint>]
let main argv =
  Program.mkProgram init update (fun model _ -> printf "%A\n" model)
  |> Program.run
  |> ignore

  Console.ReadLine()
  0