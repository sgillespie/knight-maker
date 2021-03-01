namespace MakeFs

open Godot
open MakeFs

module PopupLayoutFs =
    let buttonContainer = "PopupPanel/MarginContainer/VBoxContainer/HBoxContainer"

open PopupLayoutFs

[<AllowNullLiteral>]
type PopupLayoutFs() =
    inherit CenterContainer()

    let mutable popup: Popup = null
    let mutable quitButton: BaseButton = null
    let mutable goBackButton: BaseButton = null
    let mutable resultLabel: Label = null
    let mutable uiManager: UiManagerFs = null

    let resultText (): string =
        let state = uiManager.GameState

        state
        |> Option.bind MakeFs.gameResult
        |> fun res ->
            match res with
                | Some Won -> "You Won!"
                | Some Lost -> "You Lost"
                | None -> "???"

    [<Export>]
    member val QuitButtonPath: NodePath =
        new NodePath($"{buttonContainer}/QuitButton") with get, set
    [<Export>]
    member val GoBackButtonPath: NodePath =
        new NodePath($"{buttonContainer}/GoBackButton") with get, set
    [<Export>]
    member val PopupPath: NodePath = new NodePath("PopupPanel") with get, set
    [<Export>]
    member val ResultLabelPath: NodePath =
        new NodePath("PopupPanel/ResultLabel") with get, set

    override this._Ready() =
        popup <- this.GetNode<_>(this.PopupPath)
        quitButton <- this.GetNode<_>(this.QuitButtonPath)
        goBackButton <- this.GetNode<_>(this.GoBackButtonPath)
        resultLabel <- this.GetNode<_>(this.ResultLabelPath)
        uiManager <- UiManagerFs.get this

        quitButton.Connect("pressed", this, "_OnQuitPressed") |> ignore
        goBackButton.Connect("pressed", this, "_OnBackPressed") |> ignore

        // Container is only for layout hacking in the editor, at runtime it should always
        // be invisible
        this.Visible <- false

    override this._Process(delta: float32) =
        resultLabel.Text <- resultText ()

    member this._OnQuitPressed() =
        uiManager.Quit()

    member this._OnBackPressed() =
        popup.Hide()

    member this.Popup() =
        popup.PopupCentered()
