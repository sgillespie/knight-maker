namespace MakeFs

open Godot

type MainUiFs() =
    inherit Control()

    [<Export>]
    member val Text = "Hello World!" with get, set

    override this._Ready() =
        GD.Print(this.Text)
