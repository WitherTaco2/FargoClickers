using ClickerClass;
using ClickerClass.Dusts;
using ClickerClass.Items.Armors;
using ClickerClass.Items.Placeable;
using ClickerClass.Items.Weapons.Clickers;
using ClickerClass.Utilities;
using FargoClickers.Common;
using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.UI.Elements;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoClickers.Content.Items.Accessories.Enchantments
{
    public class MiceEnchantment : BaseEnchant
    {
        public override Color nameColor => new Color(177, 179, 224);
        public static Texture2D buffTexture;
        public override List<AccessoryEffect> ActiveSkillTooltips => [AccessoryEffectLoader.GetEffect<MiceKeyEffect>()];
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            if (!Main.dedServ)
            {
                buffTexture = ModContent.Request<Texture2D>("FargoClickers/Content/Items/Accessories/Enchantments/MiceEnchantmentBuff").Value;
            }
            ClickerSystem.RegisterClickerItem(this);
        }
        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.rare = ItemRarityID.Red;
            Item.value = 10000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<MiceEffect>(Item))
                player.AddEffect<MiceKeyEffect>(Item);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<MiceMask>()
                .AddIngredient<MiceSuit>()
                .AddIngredient<MiceBoots>()

                .AddIngredient<MiceClicker>()
                .AddIngredient<AstralClicker>()
                .AddIngredient<Galaxies>()

                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
    public class MiceEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<MatrixHeader>();
        //public override int ToggleItemType => base .ToggleItemType;
        public override int ToggleItemType => ModContent.ItemType<MiceEnchantment>();
        public override void PostUpdateEquips(Player player)
        {
            FargoClickerPlayer modPlayer = player.FargoClickerPlayer();

            if (player.HasEffect<MatrixForceEffect>())
                return;

            modPlayer.MiceEnch = true;
            if (modPlayer.miceHurtTimer > 0)
                modPlayer.miceHurtTimer--;

            if (modPlayer.miceCooldownTimer > 0 && modPlayer.miceCooldownTimerMax > 0)
            {
                modPlayer.miceCooldownTimer--;
                CooldownBarManager.Activate("MiceCooldown", ModContent.Request<Texture2D>("FargoClickers/Content/Items/Accessories/Enchantments/MiceEnchantment").Value, new Color(98, 101, 145),
                    () => Main.LocalPlayer.FargoClickerPlayer().miceCooldownTimerRatio, true, activeFunction: () => player.HasEffect<MiceEffect>());
            }
            if (modPlayer.miceCooldownTimer == 0)
            {
                //modPlayer.miceHurtTriggered = false;
                modPlayer.miceCooldownTimerMax = 0;
            }

            if (modPlayer.miceBuffTimer > 0)
            {
                modPlayer.miceBuffTimer--;
                player.moveSpeed += 0.1f;
                player.runAcceleration += 0.1f;
                CooldownBarManager.Activate("MiceBuff", MiceEnchantment.buffTexture, new Color(177, 179, 224),
                    () => (float)Main.LocalPlayer.FargoClickerPlayer().miceBuffTimer / Main.LocalPlayer.FargoClickerPlayer().miceBuffTimerMax, true, activeFunction: () => player.HasEffect<MiceEffect>());
            }
        }
        public override void OnHurt(Player player, Player.HurtInfo info)
        {
            FargoClickerPlayer modPlayer = Main.LocalPlayer.FargoClickerPlayer();

            if (player.HasEffect<MatrixForceEffect>())
                return;

            if (modPlayer.miceHurtTimer > 0 /*&& !modPlayer.miceHurtTriggered*/)
            {
                player.immuneTime = player.ForceEffect<MiceEffect>() ? 180 : 120;
                modPlayer.miceBuffTimer = modPlayer.miceBuffTimerMax;
                modPlayer.miceCooldownTimer += 29 * 60;
                modPlayer.miceCooldownTimerMax = 3600;
                modPlayer.miceHurtTimer = 0;
            }
        }
        public override float ModifyUseSpeed(Player player, Item item)
        {
            if (player.FargoClickerPlayer().miceBuffTimer > 0)
                return 0.1f;
            return 0;
        }
    }
    public class MiceKeyEffect : AccessoryEffect
    {
        public override Header ToggleHeader => null;
        public override int ToggleItemType => ModContent.ItemType<MiceEnchantment>();
        public override bool ActiveSkill => true;
        public override void ActiveSkillJustPressed(Player player, bool stunned)
        {
            if (player.FargoClickerPlayer().miceCooldownTimer != 0)
                return;

            Vector2 vector = default(Vector2);
            vector.X = Main.mouseX + Main.screenPosition.X;
            if (player.gravDir == 1f)
            {
                vector.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)player.height;
            }
            else
            {
                vector.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
            }

            vector.X -= player.width / 2;
            if (vector.X > 50f && vector.X < (float)(Main.maxTilesX * 16 - 50) && vector.Y > 50f && vector.Y < (float)(Main.maxTilesY * 16 - 50) && !Collision.SolidCollision(vector, player.width, player.height))
            {
                //Player.Teleport(vector, 4);
                //NetMessage.SendData(65, -1, -1, null, 0, player.whoAmI, vector.X, vector.Y, 1);

                //Teleport itself
                Vector2 teleportPos = Main.MouseWorld;
                SoundEngine.PlaySound(SoundID.Item115, teleportPos);

                player.ClickerTeleport(teleportPos);
                NetMessage.SendData(MessageID.PlayerControls, number: player.whoAmI);


                //Buff
                player.FargoClickerPlayer().miceHurtTimer = player.FargoClickerPlayer().miceHurtTimerMax;
                player.FargoClickerPlayer().miceCooldownTimer = 600;
                player.FargoClickerPlayer().miceCooldownTimerMax = 600;


                //Dust
                float num102 = 50f;
                int num103 = 0;
                while ((float)num103 < num102)
                {
                    Vector2 vector12 = Vector2.UnitX * 0f;
                    vector12 += -Vector2.UnitY.RotatedBy((double)((float)num103 * (MathHelper.TwoPi / num102)), default(Vector2)) * new Vector2(2f, 2f);
                    vector12 = vector12.RotatedBy((double)Vector2.Zero.ToRotation(), default(Vector2));
                    int num104 = Dust.NewDust(teleportPos, 0, 0, ModContent.DustType<MiceDust>(), 0f, 0f, 0, default(Color), 2f);
                    Main.dust[num104].noGravity = true;
                    Main.dust[num104].position = teleportPos + vector12;
                    Main.dust[num104].velocity = Vector2.Zero * 0f + vector12.SafeNormalize(Vector2.UnitY) * 4f;
                    int num = num103;
                    num103 = num + 1;
                }
            }
        }

    }
}
