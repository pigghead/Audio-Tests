using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiscordController : MonoBehaviour
{
    #region DISCORD 
    Discord.Discord d;
    Discord.UserManager u;
    Discord.ActivityManager a;
    Discord.OverlayManager o;
    Discord.VoiceManager v;
    Discord.LobbyManager lobbyManager;
    #endregion
    
    #region UI
    public Button _mute;
    public TextMeshProUGUI _muteStatus;
    #endregion

    void Start() {
        // * Create the discord manager
        d = new Discord.Discord(1042848383800463360, (uint)Discord.CreateFlags.Default);

        // * User mngmt
        u = d.GetUserManager();

        // * Activity manager for discord integration
        a = d.GetActivityManager();
        Discord.Activity activity = new Discord.Activity {
            Details = "On the GLG Discord Kitten Ranch",
            State = "Managing GLG Discord Kittens"
        };
        a.UpdateActivity(activity, (res) => {
            if (res == Discord.Result.Ok) {
                Debug.Log("Discord status set");
            } else {
                Debug.LogError("Error updating discord status");
            }
        });
        // * Teehee

        // * Overlay Manager
        o = d.GetOverlayManager();
        /*o.OpenVoiceSettings((result) => {
            if (result == Discord.Result.Ok) {
                Debug.Log("Overlay opened");
            } else {
                Debug.LogError("Error opening overlay");
            }
        });*/
        // * Useless

        // * Voice mngmt
        v = d.GetVoiceManager();
        _mute.onClick.AddListener(() => {
            v.SetSelfMute(true);
            _muteStatus.text = "Muted";
        });

        lobbyManager = d.GetLobbyManager();

        CreateLobby(lobbyManager);        
    }

    void Update() {
        d.RunCallbacks();
        //Debug.Log(GetCurrentUserData(u).Username);
    }
    
    public UserData GetCurrentUserData(Discord.UserManager u) {
        Discord.User user = u.GetCurrentUser();

        UserData udata = new UserData 
        {
            Id = user.Id,
            Username = user.Username,
            Discriminator = user.Discriminator,
            Avatar = user.Avatar,
            Bot = user.Bot
        };

        return udata;
    }

    private void CreateLobby(Discord.LobbyManager LM) {
        Discord.LobbyTransaction txn = LM.GetLobbyCreateTransaction();
        txn.SetCapacity(3);
        txn.SetType(Discord.LobbyType.Public);
        txn.SetMetadata("a", "123");

        LM.CreateLobby(txn, (Discord.Result res, ref Discord.Lobby lobby) => {
            Debug.Log($"lobby {lobby.Id} created with secret {lobby.Secret}");

            Discord.LobbyTransaction newTxn = LM.GetLobbyUpdateTransaction(lobby.Id);
            newTxn.SetCapacity(5);
            newTxn.SetOwner(1042848383800463360);

            LM.UpdateLobby(lobby.Id, newTxn, (result) => {
                if (result == Discord.Result.Ok) {
                    Debug.Log("Lobby owner updated");
                }
            });
        });
    }
}
