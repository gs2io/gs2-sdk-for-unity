using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Core.Util;
using Gs2.Editor.ResourceTree.Core;
using Gs2.Gs2Project;
using Gs2.Gs2Project.Request;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Gs2.Editor.ResourceTree
{
    public class ResourceTreeView : TreeView
    {
        internal Gs2RestSession Session;
        
        public ResourceTreeView(TreeViewState treeViewState)
            : base(treeViewState)
        {
            Reload();
        }
        
        protected override TreeViewItem BuildRoot()
        {
            return new TreeViewItem {id = 0, depth = -1, displayName = "Root"};
        }

        private Gs2Account.Gs2Account _account;
        private Gs2Chat.Gs2Chat _chat;
        private Gs2Datastore.Gs2Datastore _datastore;
        private Gs2Dictionary.Gs2Dictionary _dictionary;
        private Gs2Distributor.Gs2Distributor _distributor;
        private Gs2Enchant.Gs2Enchant _enchant;
        private Gs2Enhance.Gs2Enhance _enhance;
        private Gs2Exchange.Gs2Exchange _exchange;
        private Gs2Experience.Gs2Experience _experience;
        private Gs2Formation.Gs2Formation _formation;
        private Gs2Friend.Gs2Friend _friend;
        private Gs2Gateway.Gs2Gateway _gateway;
        private Gs2Idle.Gs2Idle _idle;
        private Gs2Inbox.Gs2Inbox _inbox;
        private Gs2Inventory.Gs2Inventory _inventory;
        private Gs2JobQueue.Gs2JobQueue _jobQueue;
        private Gs2Key.Gs2Key _key;
        private Gs2Limit.Gs2Limit _limit;
        private Gs2LoginReward.Gs2LoginReward _loginReward;
        private Gs2Lottery.Gs2Lottery _lottery;
        private Gs2Matchmaking.Gs2Matchmaking _matchmaking;
        private Gs2MegaField.Gs2MegaField _megaField;
        private Gs2Mission.Gs2Mission _mission;
        private Gs2Money.Gs2Money _money;
        private Gs2News.Gs2News _news;
        private Gs2Quest.Gs2Quest _quest;
        private Gs2Ranking.Gs2Ranking _ranking;
        private Gs2Realtime.Gs2Realtime _realtime;
        private Gs2Schedule.Gs2Schedule _schedule;
        private Gs2SerialKey.Gs2SerialKey _serialKey;
        private Gs2Showcase.Gs2Showcase _showcase;
        private Gs2Stamina.Gs2Stamina _stamina;
        private Gs2StateMachine.Gs2StateMachine _stateMachine;
        private Gs2Version.Gs2Version _version;

        protected override IList<TreeViewItem> BuildRows(TreeViewItem root)
        {
            var rows = GetRows() ?? new List<TreeViewItem>();
            rows.Clear();
            var id = 1;
            var reload = false;
            
            if (this._account == null) this._account = new Gs2Account.Gs2Account(id++);
            this._account.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._chat == null) this._chat = new Gs2Chat.Gs2Chat(id++);
            this._chat.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._datastore == null) this._datastore = new Gs2Datastore.Gs2Datastore(id++);
            this._datastore.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._dictionary == null) this._dictionary = new Gs2Dictionary.Gs2Dictionary(id++);
            this._dictionary.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._distributor == null) this._distributor = new Gs2Distributor.Gs2Distributor(id++);
            this._distributor.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._enchant == null) this._enchant = new Gs2Enchant.Gs2Enchant(id++);
            this._enchant.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._enhance == null) this._enhance = new Gs2Enhance.Gs2Enhance(id++);
            this._enhance.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._exchange == null) this._exchange = new Gs2Exchange.Gs2Exchange(id++);
            this._exchange.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._experience == null) this._experience = new Gs2Experience.Gs2Experience(id++);
            this._experience.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._formation == null) this._formation = new Gs2Formation.Gs2Formation(id++);
            this._formation.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._friend == null) this._friend = new Gs2Friend.Gs2Friend(id++);
            this._friend.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._gateway == null) this._gateway = new Gs2Gateway.Gs2Gateway(id++);
            this._gateway.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._idle == null) this._idle = new Gs2Idle.Gs2Idle(id++);
            this._idle.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._inbox == null) this._inbox = new Gs2Inbox.Gs2Inbox(id++);
            this._inbox.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._inventory == null) this._inventory = new Gs2Inventory.Gs2Inventory(id++);
            this._inventory.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._jobQueue == null) this._jobQueue = new Gs2JobQueue.Gs2JobQueue(id++);
            this._jobQueue.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._key == null) this._key = new Gs2Key.Gs2Key(id++);
            this._key.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._limit == null) this._limit = new Gs2Limit.Gs2Limit(id++);
            this._limit.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._loginReward == null) this._loginReward = new Gs2LoginReward.Gs2LoginReward(id++);
            this._loginReward.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._lottery == null) this._lottery = new Gs2Lottery.Gs2Lottery(id++);
            this._lottery.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._matchmaking == null) this._matchmaking = new Gs2Matchmaking.Gs2Matchmaking(id++);
            this._matchmaking.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._megaField == null) this._megaField = new Gs2MegaField.Gs2MegaField(id++);
            this._megaField.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._mission == null) this._mission = new Gs2Mission.Gs2Mission(id++);
            this._mission.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._money == null) this._money = new Gs2Money.Gs2Money(id++);
            this._money.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._news == null) this._news = new Gs2News.Gs2News(id++);
            this._news.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._quest == null) this._quest = new Gs2Quest.Gs2Quest(id++);
            this._quest.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._ranking == null) this._ranking = new Gs2Ranking.Gs2Ranking(id++);
            this._ranking.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._realtime == null) this._realtime = new Gs2Realtime.Gs2Realtime(id++);
            this._realtime.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._schedule == null) this._schedule = new Gs2Schedule.Gs2Schedule(id++);
            this._schedule.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._serialKey == null) this._serialKey = new Gs2SerialKey.Gs2SerialKey(id++);
            this._serialKey.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._showcase == null) this._showcase = new Gs2Showcase.Gs2Showcase(id++);
            this._showcase.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._stamina == null) this._stamina = new Gs2Stamina.Gs2Stamina(id++);
            this._stamina.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._stateMachine == null) this._stateMachine = new Gs2StateMachine.Gs2StateMachine(id++);
            this._stateMachine.Render(this, rows, this.Session, () => { reload = true; return true; });
            if (this._version == null) this._version = new Gs2Version.Gs2Version(id++);
            this._version.Render(this, rows, this.Session, () => { reload = true; return true; });
            
            if (reload) {
                Reload();
            }
            SetupDepthsFromParentsAndChildren(root);
            return rows;
        }

        protected override void ContextClickedItem(int id) {
            
            var ev = Event.current;
            ev.Use();
    
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Reload"), false,
                () =>
                {
                    var selectedObject = GetRows()
                        .FirstOrDefault(i => i.id == id);

                    if (selectedObject is AbstractTreeViewItem item) {
                        item.Reload();
                        Reload();
                    }
                }
            );
            menu.ShowAsContext();
        }
        
        protected override bool CanMultiSelect(TreeViewItem item) => false;
        protected override bool CanStartDrag(CanStartDragArgs args) => true;
        protected override void SetupDragAndDrop(SetupDragAndDropArgs args)
        {
            var selections = args.draggedItemIDs;
            if (selections.Count <= 0)
                return;

            var dragObject = GetRows()
                    .FirstOrDefault(i => selections.Contains(i.id));

            if (dragObject is AbstractTreeViewItem item) {
                var instance = item.ToScriptableObject();
                if (instance != null) {
                    DragAndDrop.PrepareStartDrag();
                    DragAndDrop.paths = null;
                    DragAndDrop.objectReferences = new Object[] { };
                    DragAndDrop.SetGenericData("Gs2Resource", instance);
                    DragAndDrop.StartDrag(dragObject.displayName);
                    Event.current.Use();
                }
            }
        }
    }
    
    class ResourceTreeWindow : EditorWindow
    {
        [SerializeField]
        private TreeViewState _treeViewState;

        private ResourceTreeView _treeView;
        
        private Vector2 _scrollPosition = Vector2.zero;

        private string _email;
        private string _password;

        private Gs2.Gs2Project.Model.Account _account;
        [SerializeField]
        private string _accountToken;
        private List<Gs2.Gs2Project.Model.Project> _projects;
        [SerializeField]
        private int _selectedProjectIndex;
        [SerializeField]
        private int _selectedRegionIndex;
        [SerializeField]
        private string _projectToken;
            
        [MenuItem ("Window/Game Server Services/Resource Tree")]
        private static void Open()
        {
            GetWindow<ResourceTreeWindow> ("GS2 Resources");
        }
        
        private void OnGUI() {
            var regions = new[] {
                Region.ApNortheast1,
                Region.UsEast1,
                Region.EuWest1,
                Region.ApSouthEast1,
            };
            if (_treeView == null) {
                if (_treeViewState == null) {
                    _treeViewState = new TreeViewState ();
                }
                _treeView = new ResourceTreeView(_treeViewState);
            } else if (_treeView.Session == null) {
                IEnumerator Open() {
                    _treeView.Session = new Gs2RestSession(
                        new ProjectTokenGs2Credential("dummy", this._projectToken ?? "dummy"),
                        regions[_selectedRegionIndex]
                    );
                    yield return _treeView.Session.OpenFuture();
                    _treeView.Reload();
                }
                RunCoroutineUtil.Run(Open());
            } else if (string.IsNullOrEmpty(this._accountToken)) {
                EditorGUILayout.LabelField("Login");
                this._email = EditorGUILayout.TextField("Email", this._email);
                this._password = EditorGUILayout.TextField("Password", this._password);
                if (GUILayout.Button("Login")) {
                    IEnumerator Login() {
                        var future = new Gs2ProjectRestClient(_treeView.Session).SignInFuture(
                            new SignInRequest()
                                .WithEmail(this._email)
                                .WithPassword(this._password)
                        );
                        yield return future;
                        if (future.Error != null) {
                            Debug.LogError(future.Error.Message);
                            yield break;
                        }
                        this._account = future.Result.Item;
                        this._accountToken = future.Result.AccountToken;
                    }
                    RunCoroutineUtil.Run(Login());
                }
            }
            else if (this._projects == null) {
                IEnumerator FetchProjects() {
                    var future = new Gs2ProjectRestClient(_treeView.Session).DescribeProjectsFuture(
                        new DescribeProjectsRequest()
                            .WithAccountToken(this._accountToken)
                    );
                    yield return future;
                    if (future.Error != null) {
                        Debug.LogError(future.Error.Message);
                        yield break;
                    }
                    this._projects = future.Result.Items.ToList();
                }
                RunCoroutineUtil.Run(FetchProjects());
            }
            else {
                if (this._account != null) {
                    EditorGUILayout.TextField("Email", this._account.Email);
                }
                if (GUILayout.Button("Logout")) {
                    this._account = null;
                    this._accountToken = null;
                    this._projects = null;
                    this._selectedProjectIndex = 0;
                    this._projectToken = null;
                }
                
                EditorGUILayout.LabelField("Project", EditorStyles.boldLabel);
                var projectNames = this._projects.Select(v => v.Name).ToList();
                projectNames.Insert(0, "Please Select Project");
                var selectedIndex = EditorGUILayout.Popup(this._selectedProjectIndex, projectNames.ToArray());
                if (_selectedProjectIndex != selectedIndex) {
                    if (selectedIndex == 0) {
                        this._projectToken = null;
                        this._selectedProjectIndex = selectedIndex;
                        this._selectedRegionIndex = 0;
                        _treeView = null;
                    }
                    else {
                        IEnumerator Login() {
                            var future = new Gs2ProjectRestClient(_treeView.Session).GetProjectTokenFuture(
                                new GetProjectTokenRequest()
                                    .WithAccountToken(this._accountToken)
                                    .WithProjectName(this._projects[selectedIndex - 1].Name)
                            );
                            yield return future;
                            if (future.Error != null) {
                                Debug.LogError(future.Error.Message);
                                yield break;
                            }
                            this._projectToken = future.Result.ProjectToken;
                            this._selectedProjectIndex = selectedIndex;

                            _treeView.Session = new Gs2RestSession(
                                new ProjectTokenGs2Credential("dummy", this._projectToken ?? "dummy"),
                                regions[_selectedRegionIndex]
                            );
                            yield return _treeView.Session.OpenFuture();

                            _treeView = null;
                        }

                        RunCoroutineUtil.Run(Login());
                    }
                }
                
                EditorGUILayout.LabelField("Region", EditorStyles.boldLabel);
                selectedIndex = EditorGUILayout.Popup(this._selectedRegionIndex, regions.Select(v => v.ToString()).ToArray());
                if (this._selectedRegionIndex != selectedIndex) {
                    this._selectedRegionIndex = selectedIndex;
                    _treeView = null;
                }

                EditorGUILayout.BeginHorizontal();
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.LabelField("Resources", EditorStyles.boldLabel);
                
                _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

                if (!string.IsNullOrEmpty(this._projectToken) && this._treeView != null) {
                    var rect = EditorGUILayout.GetControlRect(false, this._treeView.totalHeight);
                    this._treeView.OnGUI(rect);
                }
                
                EditorGUILayout.EndScrollView();
                
                if (this._treeView?.GetSelection()?.Count == 1) {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.LabelField("Inspector", EditorStyles.boldLabel);
                    var selection = this._treeView?.GetRows().FirstOrDefault(v => v.id == this._treeView?.GetSelection()[0]);
                    if (selection is AbstractTreeViewItem item) {
                        item.OnGUI();
                    }
                }
            }
        }
    }
}
