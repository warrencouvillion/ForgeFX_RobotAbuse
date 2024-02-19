# ForgeFX Programming Challenge

## Play

### Player Movement
As per the specs, moving the viewpoint uses only the keyboard. The WASD or
arrow keys move the player forward/backward/left/right. If the \<SHIFT>
key is being held down, the WA keys and up/down arrow keys will tilt the
camera, and the AS and right/left arrows will pan the camera.

The \<HOME> and <7> key on the numeric keypad will rotate the camera to look at
the robot.

### Moving the Robot and Its Arms
Clicking with the left mouse button (LMB) and dragging either robot arm will
move the arm. If the LMB is released near the shoulder, the arm will reattach.

Similarly, clicking and dragging the robot torso will move any part attached
to the torso. Is, if an arm is detached, moving the rest of the robot will
not move that arm.

### Highlighting
Only objects than can be moved by clicking and dragging are highlighted. Thus,
when an arm is detached, it will not highlight when the mouse is over the 
robot torso.

## Setup (for Level Designers)

### Make a Chained Group of Parts Movable
Parts are made movable by adding a `DragOnClick` and collider component.

Groups of parts to be moved together should be in a chain, and have the same
tag. Only the `DragOnClick` settings for the root part will be used. Child
part settings will be ignored. For example, if the root part AttachDistance
setting is 0.01, the the part will be reattached to its parent when released
within 0.01 units, even if all other parts of the group have different
settings.

If an object is tagged, but isn't directly in a chain of parts, the behavior
is undefined.

### Highlighting
To make a part highlight when the mouse is over it, and a
`HighlightOnMouseOver` component.

When any piece is highlighted, any part with the same tag is also highlighted, 
whether a `HighlightOnMouseOver` is attached or not.

When a `HighlightOnMouseOver` is attached to an untagged part *all* parts will
be highlighted whenthat part is.

The `HighlightOnMouseOver.OnlyHighlightChildren` flag indicates that only child
parts of the highlighted part will also be highlighted. For example, if a part
is detached from its tag group, it will not be highlighted when other parts of
the group are.

## Testing

### EditMode Tests

There are editor tests for checking if 
 - All objects with a `DragOnClick` component also have a 
 `HighlightOnMouseOver` component.
 - All objects with a `DragOnClick` component also have a collider component.

### PlayMode Tests

There are play mode tests for the UFO motion to check:
 - That the \<HOME> key rotates the object to look at a target
 - That the "Horizontal" and "Vertical" axes (as set in the Input section of
 the project settings) move the object properly
 - That the "Horizontal" and "Vertical" axes rotate the object properly if a 
 \<SHIFT> key is held down.
