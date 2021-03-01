namespace MakeFs

open Godot

[<AllowNullLiteral>]
type ChoiceUiFs() =
    inherit VBoxContainer()

    let mutable button1: BaseButton = null
    let mutable button2: BaseButton = null
    let mutable button3: BaseButton = null
    let mutable label1: Label = null
    let mutable label2: Label = null
    let mutable label3: Label = null

    let buttonText
        (v: string)
        (buttonVal: string)
        (buttonText: string) : string =

        if v = buttonVal then
            $"{buttonText}*"
        else
            buttonText

    [<Export>]
    member val Button1Text: string = "Option 1" with get, set

    [<Export>]
    member val Button1Val: string = "Option1" with get, set

    [<Export>]
    member val Button2Text: string = "Option 2" with get, set

    [<Export>]
    member val Button2Val: string = "Option2" with get, set

    [<Export>]
    member val Button3Text: string = "Option 3" with get, set

    [<Export>]
    member val Button3Val: string = "Option3" with get, set

    override this._Ready() =
        let path = "VBoxContainer"
        
        button1 <- this.GetNode<BaseButton>(new NodePath($"{path}/Button1"))
        button2 <- this.GetNode<BaseButton>(new NodePath($"{path}/Button2"))
        button3 <- this.GetNode<BaseButton>(new NodePath($"{path}/Button3"))
        label1 <- this.GetNode<Label>(new NodePath($"{path}/Button1/Label"))
        label2 <- this.GetNode<Label>(new NodePath($"{path}/Button2/Label"))
        label3 <- this.GetNode<Label>(new NodePath($"{path}/Button3/Label"))

        label1.Text <- this.Button1Text
        label2.Text <- this.Button2Text
        label3.Text <- this.Button3Text

        // Wire the signal handlers
        button1.Connect(
            "pressed",
            this,
            "_OnOptionClicked",
            new Godot.Collections.Array([this.Button1Val])) |> ignore
        button2.Connect(
            "pressed",
            this,
            "_OnOptionClicked",
            new Godot.Collections.Array([this.Button2Val])) |> ignore
        button3.Connect(
            "pressed",
            this,
            "_OnOptionClicked",
            new Godot.Collections.Array([this.Button3Val])) |> ignore

        button1.Connect(
            "mouse_entered",
            this,
            "_OnButtonMouseEnter",
            new Godot.Collections.Array([this.Button1Val])) |> ignore
        button2.Connect(
            "mouse_entered",
            this,
            "_OnButtonMouseEnter",
            new Godot.Collections.Array([this.Button2Val])) |> ignore
        button3.Connect(
            "mouse_entered",
            this,
            "_OnButtonMouseEnter",
            new Godot.Collections.Array([this.Button3Val])) |> ignore

        this.AddUserSignal("selection_changed")
        this.AddUserSignal("button_mouse_enter")

    member this._OnOptionClicked(v: string) =
        this.EmitSignal("selection_changed", v)

        let buttonText' = buttonText v
        
        label1.Text <- buttonText' this.Button1Val this.Button1Text
        label2.Text <- buttonText' this.Button2Val this.Button2Text
        label3.Text <- buttonText' this.Button3Val this.Button3Text

    member this._OnButtonMouseEnter(v: string) =
        this.EmitSignal("button_mouse_enter", v)
