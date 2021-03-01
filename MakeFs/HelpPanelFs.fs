namespace MakeFs

open Godot

open MakeFs
open MakeFs.Domain.Character

type HelpPanelFs() =
    inherit MarginContainer()

    let mutable button: BaseButton = null
    let mutable enemyLabel: RichTextLabel = null
    let mutable helpLabel: RichTextLabel = null
    let mutable uiManager: UiManagerFs = null

    let labelText () =
        uiManager.GameState
        |> Option.map (fun st -> st.Enemy)
        |> fun enemy ->
            match enemy with
                | Some e ->
                    $"Name: {e.Name}\n\
                      Build: {e.Build}\n\
                      Weapon: {e.Weapon}\n\
                      Modifier: {e.Modifier}"
                | None -> ""
    
    [<Export>]
    member val SubmitButton: NodePath = null with get, set
    [<Export>]
    member val EnemyBuildLabel: NodePath = null with get, set
    [<Export>]
    member val HelpLabel: NodePath = null with get, set

    override this._Ready() =
        button <- this.GetNode<_>(this.SubmitButton)
        enemyLabel <- this.GetNode<_>(this.EnemyBuildLabel)
        helpLabel <- this.GetNode<_>(this.HelpLabel)
        uiManager <- UiManagerFs.get this

        button.Connect("pressed", this, "_OnBattlePressed") |> ignore
        uiManager.Connect("build_mouse_enter", this, "_OnBuildHover") |> ignore
        uiManager.Connect("weapon_mouse_enter", this, "_OnWeaponHover") |> ignore
        uiManager.Connect("modifier_mouse_enter", this, "_OnModifierHover") |> ignore

    override this._Process(delta: float32) =
        enemyLabel.Text <- labelText ()

    member this._OnBattlePressed() =
        uiManager.EmitSignal("battle_pressed")

    member this._OnBuildHover(build: BuildWrapped) =
        helpLabel.Text <- build.Value.HelpText()

    member this._OnWeaponHover(weapon: WeaponWrapped) =
        helpLabel.Text <- weapon.Value.HelpText()

    member this._OnModifierHover(modifier: ModifierWrapped) =
        helpLabel.Text <- modifier.Value.HelpText()
