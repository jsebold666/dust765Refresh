#region license

// Copyright (c) 2021, andreakarasho
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
// 1. Redistributions of source code must retain the above copyright
//    notice, this list of conditions and the following disclaimer.
// 2. Redistributions in binary form must reproduce the above copyright
//    notice, this list of conditions and the following disclaimer in the
//    documentation and/or other materials provided with the distribution.
// 3. All advertising materials mentioning features or use of this software
//    must display the following acknowledgement:
//    This product includes software developed by andreakarasho - https://github.com/andreakarasho
// 4. Neither the name of the copyright holder nor the
//    names of its contributors may be used to endorse or promote products
//    derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS ''AS IS'' AND ANY
// EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
// ## BEGIN - END ## // NAMEOVERHEAD
using System.Collections.Generic;
// ## BEGIN - END ## // NAMEOVERHEAD
// ## BEGIN - END ## // TAZUO
using ClassicUO.Configuration;
// ## BEGIN - END ## // TAZUO
using ClassicUO.Game.Managers;
using ClassicUO.Game.UI.Controls;
using ClassicUO.Resources;
using Microsoft.Xna.Framework;

namespace ClassicUO.Game.UI.Gumps
{
    internal class NameOverHeadHandlerGump : Gump
    {
        public static Point? LastPosition;

        public override GumpType GumpType => GumpType.NameOverHeadHandler;
        // ## BEGIN - END ## // NAMEOVERHEAD
        private readonly List<RadioButton> _overheadButtons = new List<RadioButton>();
        private Control _alpha;
        private readonly Checkbox _keepOpenCheckbox;
        private readonly Checkbox _keepPinnedCheckbox;
        private readonly Checkbox _noBackgroundCheckbox;
        private readonly Checkbox _healthLinesCheckbox;
        // ## BEGIN - END ## // NAMEOVERHEAD


        public NameOverHeadHandlerGump() : base(0, 0)
        {
            CanMove = true;
            AcceptMouseInput = true;
            // ## BEGIN - END ## // NAMEOVERHEAD
            //CanCloseWithRightClick = true;
            // ## BEGIN - END ## // NAMEOVERHEAD
            if (NameOverHeadManager.IsPinnedToggled)
            {
                CanCloseWithRightClick = false;
            }
            else
            {
                CanCloseWithRightClick = true;
            }
            // ## BEGIN - END ## // NAMEOVERHEAD

            if (LastPosition == null)
            {
                X = 100;
                Y = 100;
            }
            else
            {
                X = LastPosition.Value.X;
                Y = LastPosition.Value.Y;
            }

            WantUpdateSize = false;

            LayerOrder = UILayer.Over;

            // ## BEGIN - END ## // NAMEOVERHEAD
            /*
            // ## BEGIN - END ## // TAZUO
            Checkbox stayActive;
            // ## BEGIN - END ## // TAZUO
            RadioButton all, mobiles, items, mobilesCorpses;
            AlphaBlendControl alpha;

            Add
            (
                alpha = new AlphaBlendControl(0.7f)
                {
                    Hue = 34
                }
            );

            // ## BEGIN - END ## // TAZUO
            Add
            (
                stayActive = new Checkbox
                (
                    0x00D2,
                    0x00D3,
                    "Stay active",
                    color: 0xFFFF
                )
                {
                    IsChecked = ProfileManager.CurrentProfile.NameOverheadToggled,
                }
            );
            stayActive.ValueChanged += (sender, e) => { ProfileManager.CurrentProfile.NameOverheadToggled = stayActive.IsChecked; };
            // ## BEGIN - END ## // TAZUO

            Add
            (
                all = new RadioButton
                (
                    0,
                    0x00D0,
                    0x00D1,
                    ResGumps.All,
                    color: 0xFFFF
                )
                {
                    // ## BEGIN - END ## // TAZUO
                    Y = stayActive.Height + stayActive.Y,
                    // ## BEGIN - END ## // TAZUO
                    IsChecked = NameOverHeadManager.TypeAllowed == NameOverheadTypeAllowed.All
                }
            );

            Add
            (
                mobiles = new RadioButton
                (
                    0,
                    0x00D0,
                    0x00D1,
                    ResGumps.MobilesOnly,
                    color: 0xFFFF
                )
                {
                    Y = all.Y + all.Height,
                    IsChecked = NameOverHeadManager.TypeAllowed == NameOverheadTypeAllowed.Mobiles
                }
            );

            Add
            (
                items = new RadioButton
                (
                    0,
                    0x00D0,
                    0x00D1,
                    ResGumps.ItemsOnly,
                    color: 0xFFFF
                )
                {
                    Y = mobiles.Y + mobiles.Height,
                    IsChecked = NameOverHeadManager.TypeAllowed == NameOverheadTypeAllowed.Items
                }
            );

            Add
            (
                mobilesCorpses = new RadioButton
                (
                    0,
                    0x00D0,
                    0x00D1,
                    ResGumps.MobilesAndCorpsesOnly,
                    color: 0xFFFF
                )
                {
                    Y = items.Y + items.Height,
                    IsChecked = NameOverHeadManager.TypeAllowed == NameOverheadTypeAllowed.MobilesCorpses
                }
            );

            alpha.Width = Math.Max(mobilesCorpses.Width, Math.Max(items.Width, Math.Max(all.Width, mobiles.Width)));
            // ## BEGIN - END ## // TAZUO
            //alpha.Height = all.Height + mobiles.Height + items.Height + mobilesCorpses.Height;
            // ## BEGIN - END ## // TAZUO
            alpha.Height = stayActive.Height + all.Height + mobiles.Height + items.Height + mobilesCorpses.Height;
            // ## BEGIN - END ## // TAZUO

            Width = alpha.Width;
            Height = alpha.Height;

            all.ValueChanged += (sender, e) =>
            {
                if (all.IsChecked)
                {
                    NameOverHeadManager.TypeAllowed = NameOverheadTypeAllowed.All;
                }
            };

            mobiles.ValueChanged += (sender, e) =>
            {
                if (mobiles.IsChecked)
                {
                    NameOverHeadManager.TypeAllowed = NameOverheadTypeAllowed.Mobiles;
                }
            };

            items.ValueChanged += (sender, e) =>
            {
                if (items.IsChecked)
                {
                    NameOverHeadManager.TypeAllowed = NameOverheadTypeAllowed.Items;
                }
            };

            mobilesCorpses.ValueChanged += (sender, e) =>
            {
                if (mobilesCorpses.IsChecked)
                {
                    NameOverHeadManager.TypeAllowed = NameOverheadTypeAllowed.MobilesCorpses;
                }
            };
            */
            // ## BEGIN - END ## // NAMEOVERHEAD
            Add
            (
                _alpha = new AlphaBlendControl(0.7f)
                {
                    Hue = 34
                }
            );

            Add
            (
                _keepOpenCheckbox = new Checkbox
                (
                    0x00D2, 0x00D3, "Keep open", 0xFF,
                    0xFFFF
                )
                {
                    IsChecked = NameOverHeadManager.IsPermaToggled
                }
            );

            _keepOpenCheckbox.ValueChanged += (sender, args) => NameOverHeadManager.SetOverheadToggled(_keepOpenCheckbox.IsChecked);

            Add
            (
                _keepPinnedCheckbox = new Checkbox
                (
                    0x00D2, 0x00D3, "Pin this UI", 0xFF,
                    0xFFFF
                )
                {
                    IsChecked = NameOverHeadManager.IsPinnedToggled,
                    X = 100
                }
            );

            _keepPinnedCheckbox.ValueChanged += (sender, args) => NameOverHeadManager.SetPinnedToggled(_keepPinnedCheckbox.IsChecked);

            Add
            (
                _noBackgroundCheckbox = new Checkbox
                (
                    0x00D2, 0x00D3, "bg on mouse", 0xFF,
                    0xFFFF
                )
                {
                    IsChecked = NameOverHeadManager.IsBackgroundToggled,
                    Y = 20
                }
            );

            _noBackgroundCheckbox.ValueChanged += (sender, args) => NameOverHeadManager.SetBackgroundToggled(_noBackgroundCheckbox.IsChecked);

            Add
            (
                _healthLinesCheckbox = new Checkbox
                (
                    0x00D2, 0x00D3, "HP", 0xFF,
                    0xFFFF
                )
                {
                    IsChecked = NameOverHeadManager.IsHealthLinesToggled,
                    Y = 20,
                    X = 100
                }
            );

            _healthLinesCheckbox.ValueChanged += (sender, args) => NameOverHeadManager.SetHealthLinesToggled(_healthLinesCheckbox.IsChecked);

            DrawChoiceButtons();
            // ## BEGIN - END ## // NAMEOVERHEAD
        }


        protected override void OnDragEnd(int x, int y)
        {
            LastPosition = new Point(ScreenCoordinateX, ScreenCoordinateY);

            SetInScreen();

            base.OnDragEnd(x, y);
        }

        // ## BEGIN - END ## // NAMEOVERHEAD
        public void UpdateCheckboxes()
        {
            foreach (var button in _overheadButtons)
            {
                button.IsChecked = NameOverHeadManager.LastActiveNameOverheadOption == button.Text;
            }

            _keepOpenCheckbox.IsChecked = NameOverHeadManager.IsPermaToggled;
            _keepPinnedCheckbox.IsChecked = NameOverHeadManager.IsPinnedToggled;
            _noBackgroundCheckbox.IsChecked = NameOverHeadManager.IsBackgroundToggled;
            _healthLinesCheckbox.IsChecked = NameOverHeadManager.IsHealthLinesToggled;
        }
        public void RedrawOverheadOptions()
        {
            foreach (var button in _overheadButtons)
                Remove(button);

            DrawChoiceButtons();
        }

        private void DrawChoiceButtons()
        {
            int biggestWidth = 100;
            var options = NameOverHeadManager.GetAllOptions();

            for (int i = 0; i < options.Count; i++)
            {
                biggestWidth = Math.Max(biggestWidth, AddOverheadOptionButton(options[i], i).Width);
            }

            _alpha.Width = biggestWidth;
            _alpha.Height = Math.Max(30, options.Count * 20) + 42;

            Width = _alpha.Width;
            Height = _alpha.Height;
        }

        private RadioButton AddOverheadOptionButton(NameOverheadOption option, int index)
        {
            RadioButton button;

            Add
            (
                button = new RadioButton
                (
                    0, 0x00D0, 0x00D1, option.Name,
                    color: 0xFFFF
                )
                {
                    Y = 20 * index + 42,
                    IsChecked = NameOverHeadManager.LastActiveNameOverheadOption == option.Name,
                }
            );

            button.ValueChanged += (sender, e) =>
            {
                if (button.IsChecked)
                {
                    NameOverHeadManager.SetActiveOption(option);
                }
            };

            _overheadButtons.Add(button);

            return button;
        }
        // ## BEGIN - END ## // NAMEOVERHEAD
    }
}
