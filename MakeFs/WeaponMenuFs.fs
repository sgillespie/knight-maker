namespace MakeFs

open Godot
open MakeFs
open MakeFs.Domain.Character

[<AllowNullLiteral>]
type WeaponMenuFs() =
    inherit ChoiceUiFs()

    let mutable uiManager: UiManagerFs = null

    override this._EnterTree() =
        this.Button1Text <- "Sword"
        this.Button2Text <- "Axe"
        this.Button3Text <- "Warhammer"
        this.Button1Val <- "Sword"
        this.Button2Val <- "Axe"
        this.Button3Val <- "Warhammer"

    override this._Ready() =
        base._Ready()
        uiManager <- UiManagerFs.get this
        this.Connect("selection_changed", this, "_WeaponChanged") |> ignore
        this.Connect("button_mouse_enter", this, "_WeaponMouseEnter") |> ignore

    member this._WeaponChanged(v: string) =
        uiManager.EmitSignal("weapon_changed", new WeaponWrapped(weapon v))

    member this._WeaponMouseEnter(v: string) =
        uiManager.EmitSignal("weapon_mouse_enter", new WeaponWrapped(weapon v))
