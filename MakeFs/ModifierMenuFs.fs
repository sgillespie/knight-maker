namespace MakeFs

open Godot

open MakeFs
open MakeFs.Domain.Character

[<AllowNullLiteral>]
type ModifierMenuFs() =
    inherit ChoiceUiFs()

    let mutable uiManager: UiManagerFs = null

    override this._EnterTree() =
        this.Button1Text <- "Fire"
        this.Button2Text <- "Ice"
        this.Button3Text <- "Lightning"
        this.Button1Val <- "Fire"
        this.Button2Val <- "Ice"
        this.Button3Val <- "Lightning"

    override this._Ready() =
        base._Ready()
        uiManager <- UiManagerFs.get this
        this.Connect("selection_changed", this, "_ModifierChanged") |> ignore
        this.Connect("button_mouse_enter", this, "_ModifierMouseEnter") |> ignore

    member this._ModifierChanged(v: string) =
        uiManager.EmitSignal("modifier_changed", new ModifierWrapped(modifier v))

    member this._ModifierMouseEnter(v: string) =
        uiManager.EmitSignal("modifier_mouse_enter", new ModifierWrapped(modifier v))
