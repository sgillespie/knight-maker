using Godot;
using System;

using MakeFs;

public class ChoiceUi : ChoiceUiFs {
    [Signal]
    public delegate void selection_changed(String v);

    [Signal]
    public delegate void button_mouse_enter(String v);
}
