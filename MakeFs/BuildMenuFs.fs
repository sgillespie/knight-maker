namespace MakeFs

open Godot
open MakeFs
open MakeFs.Domain.Character

[<AllowNullLiteral>]
type BuildMenuFs() =
    inherit ChoiceUiFs()

    let mutable uiManager: UiManagerFs = null

    override this._EnterTree() =
        this.Button1Text <- "Strength"
        this.Button2Text <- "Vitality"
        this.Button3Text <- "Skill"
        this.Button1Val <- "Strength"
        this.Button2Val <- "Vitality"
        this.Button3Val <- "Skill"

    override this._Ready() =
        base._Ready()
        uiManager <- UiManagerFs.get this
        this.Connect("selection_changed", this, "_BuildChanged") |> ignore
        this.Connect("button_mouse_enter", this, "_BuildMouseEnter") |> ignore

    member this._BuildChanged(v: string) =
        uiManager.EmitSignal("build_changed", new BuildWrapped(build v))

    member this._BuildMouseEnter(v: string) =
        uiManager.EmitSignal("build_mouse_enter", new BuildWrapped(build v))
