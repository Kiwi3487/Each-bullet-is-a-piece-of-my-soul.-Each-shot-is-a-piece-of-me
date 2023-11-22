using UnityEngine;
public static class InputManager
{
    private static Controls _ctrls;
    private static Vector3 _mousePos;
    private static Camera cam;
    public static Ray GetCameraRay()
    {
        return cam.ScreenPointToRay(_mousePos);
    }
    public static void Init(Player p)
    {
        _ctrls = new();
        
        cam = Camera.main;
        
        _ctrls.Permenanet.Enable();
        _ctrls.InGame.Shoot.performed += _ =>
        {
            p.Shoot();
        };
        _ctrls.Permenanet.MousePos.performed += ctx =>
        {
            _mousePos = ctx.ReadValue<Vector2>();
        };
        
        _ctrls.InGame.Movement.performed += hi =>
        {
            p.SetMovementDirection(hi.ReadValue<Vector3>());
        };

        _ctrls.InGame.Jump.started += hello =>
        {
            p.Jump();
        };
        
        _ctrls.InGame.Look.performed += ctx =>
        {
            p.SetLookRotation(ctx.ReadValue<Vector2>());
        };
        
        _ctrls.InGame.Reload.started += hello =>
        {
            p.Reload();
        };
        
        _ctrls.InGame.ShotGunWeapon.started += ctx =>
        {
            p.ShotGunWeapon();
        };
        
        _ctrls.InGame.BurstFireWeapon.started += ctx =>
        {
            p.BurstFireWeapon();
        };
        
        _ctrls.InGame.SingleFireWeapon.started += ctx =>
        {
            p.SingleFireWeapon();
        };

        
    }
    public static void EnableInGame()
    {
        _ctrls.InGame.Enable();
    }
}