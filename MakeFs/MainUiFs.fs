namespace MakeFs

open Godot

open MakeFs
open MakeFs.Domain

type MainUiFs() =
    inherit Control()

    let defaultPlayerName: string = "You"
    let defaultEnemyName: string = "Dragon"

    let mutable popupContainer: PopupLayoutFs = null
    let mutable uiManager: UiManagerFs = null

    [<Export>]
    member val PlayerName: string = defaultPlayerName with get, set
    [<Export>]
    member val EnemyName: string = defaultEnemyName with get, set
    [<Export>]
    member val PopupContainerPath: NodePath =
        new NodePath("PopupContainer") with get, set

    override this._Ready() =
        popupContainer <- this.GetNode<PopupLayoutFs>(this.PopupContainerPath)
        uiManager <- UiManagerFs.get this

        // Wire up game manager
        uiManager.GameState <-
            Some (GameState.Init uiManager.Rand this.PlayerName this.EnemyName)
        uiManager.Connect("battle_pressed", this, "_OnBattlePressed") |> ignore

    member this._OnBattlePressed() =
        uiManager.GameState
        |> Option.bind (fun st -> st.Player())
        |> fun player ->
            match player with
                | Some pl -> popupContainer.Popup()
                | None -> GD.Print("Warning: Player Build not complete")
