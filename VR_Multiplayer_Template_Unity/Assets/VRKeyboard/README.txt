# VRKeyboard
Tiny and lightweight VR Keyboard Gazing Control/Laser Pointer solution.
Tested on Oculus and GearVR.

### Steps:
1. Make sure there is EventSystem Gameobject in your scene.
2. Drag FormKeyboardL1/FormKeyboardL2 Prefab in your scene.
3. For Gaze: Drag GazeRaycaster to your camera, you might need to adjust the position.
4. For laser pointer(Oculus): Drag Laser Prefab to your controller, and set the trigger key. <BR>
5. Set the EventSystem > "First Selected" to your first selected VRInputInputInteractable in the Form.
6. You can drag multiple VRInputInteractable prefabs to your Form.

### Note:
1. (Highly recommended) You can remove the GazeRaycaster script in GazeRaycaster object,
and put the script on your camera. In that way, the start position of ray will be your camera position.
2. If you want to use GazeRaycaster to trigger other event,
simply add your gameobject with VRGazeInteractable tag, then add an Button click event to your object.
Because the idea behind the GazeRaycaster is to invoke an Button.onClick() event.

### Adjustment:
In Keyboard > KeyboardManager, you can set if keyboard is uppercase at initialization.

In GazeRaycaster,
You can adjust the loaidng time (ie. circle filling time).

If you have any issues, please contact yunhn.lee@gmail.com.